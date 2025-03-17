using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Options;

namespace OKX.Net.SymbolOrderBooks
{
    /// <summary>
    /// Live order book implementation
    /// </summary>
    public class OKXSymbolOrderBook : SymbolOrderBook
    {
        private readonly IOKXSocketClient _socketClient;
        private readonly bool _clientOwner;
        private bool _initialSnapshotDone;
        private int? _levels;
        private bool _snapshots;
        private OrderBookType _type;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public OKXSymbolOrderBook(string symbol, Action<OKXOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null)
        {
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="socketClient">Socket client instance</param>
        public OKXSymbolOrderBook(string symbol,
            Action<OKXOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IOKXSocketClient? socketClient) : base(logger, "OKX", "Unified", symbol)
        {
            var options = OKXOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _sequencesAreConsecutive = false;
            _strictLevels = true;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _levels = options?.Limit;

            _socketClient = socketClient ?? new OKXSocketClient();
            _clientOwner = socketClient == null;


            _levels?.ValidateIntValues("Limit", 1, 5, 400);
            _type = OrderBookType.BBO_TBT;
            if (_levels == 5)
                _type = OrderBookType.OrderBook_5;
            else if (_levels == 400 || _levels == null)
                _type = OrderBookType.OrderBook;

            _snapshots = _type == OrderBookType.OrderBook_5 || _type == OrderBookType.BBO_TBT;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {

            var result = await _socketClient.UnifiedApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(Symbol, _type, ProcessUpdate).ConfigureAwait(false);
            if (!result)
                return result;

            if (ct.IsCancellationRequested)
            {
                await result.Data.CloseAsync().ConfigureAwait(false);
                return result.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;

            var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            return setResult ? result : new CallResult<UpdateSubscription>(setResult.Error!);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
            _initialSnapshotDone = false;
        }

        private void ProcessUpdate(DataEvent<OKXOrderBook> data)
        {
            if (!_initialSnapshotDone || _snapshots)
            {
                SetInitialOrderBook(data.Data.Time.Ticks, data.Data.Bids, data.Data.Asks);
                _initialSnapshotDone = true;
            }
            else
            {
                UpdateOrderBook(data.Data.Time.Ticks, data.Data.Bids, data.Data.Asks);
                //AddChecksum((int)data.Data.Checksum!);
            }
        }

        ///// <inheritdoc />
        //protected override bool DoChecksum(int checksum)
        //{
        //}

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
                _socketClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}
