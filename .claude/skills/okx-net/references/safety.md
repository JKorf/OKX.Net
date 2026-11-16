# OKX operational considerations

## Credentials and environment

Use key, secret and passphrase from secret storage. Match credential permissions and regional/demo environment to the intended account. Demo requires explicit configuration and suitable credentials; changing an instrument type does not make a request simulated. Trading permission does not imply withdrawals, borrowing or subaccount administration.

## Orders and nested outcomes

`CheckOrderAsync` checks an order without submitting it; it does not reserve balances or guarantee later acceptance. `PlaceOrderAsync`, including socket requests, changes account state. A limit order can fill immediately and cancellation can race with fills. Reconcile order status and executed quantity/trades to establish exposure.

Native socket order methods require numeric instrument codes, not display symbols. Check the metadata lookup and nullable code before sending. REST/socket batches can return per-item `CallResult` failures despite outer success; inspect every item and retain client/order identifiers before deciding which requests remain unresolved. Ordinary orders and algo orders have distinct query/cancel paths. An accepted close request does not prove exposure is gone.

## Account and position modes

Read account configuration and current positions before choosing account mode, position mode, trade mode, margin mode, position side or leverage. Spot cash orders use `TradeMode.Cash`; cross/isolated margin orders have different funding and borrowing behavior. Net versus long/short position mode changes valid position-side and reduce-only semantics. Derivatives size is contract quantity; respect contract value, lot steps and settlement assets. Avoid changing account/position mode automatically to make an example work.

Leverage, margin adjustment, close-position, borrow/repay and auto-repay settings alter live risk. Close only the requested position using its current side/quantity and appropriate mode. Cancel-after timers have their own lifetime and scope; do not assume they protect every order category without checking.

## Funding, subaccounts and copy trading

Withdrawals require asset, network, destination type/address, fee and quantity to match the intended movement. Transfers distinguish funding/trading accounts and subaccounts. Dust conversion and savings redemption/purchase also change balances. Subaccount API key creation/reset/deletion and transfer-out permissions change account access. Copy-trading stop/close/amend operations affect lead/copy positions and are separate from ordinary order management.

## Uncertain submissions and retries

A timeout or connection loss can leave a request accepted but unconfirmed. `Error.IsTransient` alone does not justify repeating orders, withdrawals, transfers, cancellations or mode changes. Reconcile retained client/order IDs, trades, account state and transfer/withdrawal history before deciding whether to resubmit; allow for propagation delays and partial batches. Bounded backoff is suitable for transient public reads. Keep generic read-retry helpers away from account-changing requests.
