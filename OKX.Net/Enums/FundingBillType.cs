﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum FundingBillType
{
    [Map("1")]
    Deposit,
    [Map("2")]
    Withdrawal,
    [Map("13")]
    CanceledWithdrawal,
    [Map("20")]
    TransferToSubAccount,
    [Map("21")]
    TransferFromSubAccount,
    [Map("22")]
    TransferToMasterAccount,
    [Map("23")]
    TransferFromMasterAccount,
    [Map("28")]
    Claim,
    [Map("47")]
    SystemReversal,
    [Map("48")]
    ReceivedFromActivities,
    [Map("49")]
    GivenAwayToActivities,
    [Map("50")]
    ReceivedFromAppointments,
    [Map("51")]
    DeductedFromAppointments,
    [Map("52")]
    RedPacketSent,
    [Map("53")]
    RedPacketSnatched,
    [Map("54")]
    RedPacketRefunded,
    [Map("61")]
    Conversion,
    [Map("68")]
    ClaimRebateCard,
    [Map("69")]
    DistributeRebateCard,
    [Map("72")]
    TokenReceived,
    [Map("73")]
    TokenGivenAway,
    [Map("74")]
    TokenRefunded,
    [Map("75")]
    SubscriptionToSavings,
    [Map("76")]
    RedemptionToSavings,
    [Map("77")]
    Distribute,
    [Map("78")]
    LockUp,
    [Map("79")]
    NodeVoting,
    [Map("80")]
    StakingPurchase,
    [Map("81")]
    VoteRedemption,
    [Map("82")]
    StakingRedemption,
    [Map("83")]
    StakingYield,
    [Map("84")]
    ViolationFee,
    [Map("85")]
    PowMiningYield,
    [Map("86")]
    CloudMiningPay,
    [Map("87")]
    CloudMiningYield,
    [Map("88")]
    Subsidy,
    [Map("89")]
    Staking89,
    [Map("92")]
    AddCollateral,
    [Map("93")]
    RedeemCollateral,
    [Map("94")]
    Investment,
    [Map("95")]
    BorrowerBorrows,
    [Map("96")]
    PrincipalTransferredIn,
    [Map("97")]
    BorrowerTransferredLoanOut,
    [Map("98")]
    BorrowerTransferredInterestOut,
    [Map("99")]
    InvestorTransferredInterestIn,
    [Map("102")]
    PrepaymentPenaltyTransferredIn,
    [Map("103")]
    PrepaymentPenaltyTransferredOut,
    [Map("104")]
    FeeTransferredIn,
    [Map("105")]
    FeeTransferredOut,
    [Map("106")]
    OverdueFeeTransferredIn,
    [Map("107")]
    OverdueFeeTransferredOut,
    [Map("108")]
    OverdueInterestTransferredOut,
    [Map("109")]
    OverdueInterestTransferredIn,
    [Map("110")]
    CollateralForClosedPositionTransferredIn,
    [Map("111")]
    CollateralForClosedPositionTransferredOut,
    [Map("112")]
    CollateralForLiquidationTransferredIn,
    [Map("113")]
    CollateralForLiquidationTransferredOut,
    [Map("114")]
    InsuranceFundTransferredIn,
    [Map("115")]
    InsuranceFundTransferredOut,
    [Map("116")]
    PlaceAnOrder,
    [Map("117")]
    FulfillAnOrder,
    [Map("118")]
    CancelAnOrder,
    [Map("119")]
    MerchantsUnlockDeposit,
    [Map("120")]
    MerchantsAddDeposit,
    [Map("121")]
    FiatgatewayPlaceAnOrder,
    [Map("122")]
    FiatgatewayCancelAnOrder,
    [Map("123")]
    FiatgatewayFulfillAnOrder,
    [Map("124")]
    JumpstartUnlocking,
    [Map("125")]
    ManualDeposit,
    [Map("126")]
    InterestDeposit,
    [Map("127")]
    InvestmentFeeTransferredIn,
    [Map("128")]
    InvestmentFeeTransferredOut,
    [Map("129")]
    RewardsTransferredIn,
    [Map("130")]
    TransferredFromUnifiedAccount,
    [Map("131")]
    TransferredToUnifiedAccount,
    [Map("132")]
    CustomerServiceFrozen,
    [Map("133")]
    CustomerServiceUnfrozen,
    [Map("134")]
    CustomerServiceTransfered,
    [Map("135")]
    CrossChainExchange,
    [Map("136")]
    EthStakingIncrease,
    [Map("137")]
    EthStakingSubscription,
    [Map("138")]
    EthStakingSwapping,
    [Map("139")]
    EthStakingEarnings,
    [Map("143")]
    SystemReverse,
    [Map("144")]
    ReserveTransferOut,
    [Map("145")]
    RewardExpired,
    [Map("146")]
    CustomerFeedback,
    [Map("147")]
    VestedSushiReward,
    [Map("150")]
    AffiliateCommission,
    [Map("151")]
    ReferralReward,
    [Map("152")]
    BrokerReward,
    [Map("153")]
    JoinerReward,
    [Map("154")]
    MysteryBoxReward,
    [Map("155")]
    RewardsWithdraw,
    [Map("160")]
    DualInvestSubscribe,
    [Map("161")]
    DualInvestCollection,
    [Map("162")]
    DualInvestProfit,
    [Map("163")]
    DualInvestRefund,
    [Map("169")]
    NewYearReward2022,
    [Map("172")]
    SubAffiliateCommission,
    [Map("173")]
    FeeRebate,
    [Map("174")]
    Pay,
    [Map("175")]
    LockedCollateral,
    [Map("176")]
    Loan,
    [Map("177")]
    AddedCollateral,
    [Map("178")]
    ReturnedCollateral,
    [Map("179")]
    Repayment,
    [Map("180")]
    UnlockCollateral,
    [Map("181")]
    AirdropPayment,
    [Map("182")]
    FeebackBounty,
    [Map("183")]
    InviteFriendsReward,
    [Map("184")]
    RewardPoolRevite,
    [Map("185")]
    BrokerConvertReward,
    [Map("186")]
    FreeEth,
    [Map("187")]
    ConvertTransfer,
    [Map("188")]
    SlotAuctionConversion,
    [Map("189")]
    MysteryBoxBonus,
    [Map("193")]
    CardPaymentBuy,
    [Map("195")]
    UntradableAssetWithdraw,
    [Map("196")]
    UntradableAssetWithdrawRevoked,
    [Map("197")]
    UntradableAssetDeposit,
    [Map("198")]
    UntradableAssetCollectionReduce,
    [Map("199")]
    UntradableAssetCollectionIncrease,
    [Map("200")]
    Buy,
    [Map("202")]
    PriceLockSubscribe,
    [Map("203")]
    PriceLockCollection,
    [Map("204")]
    PriceLockProfit,
    [Map("205")]
    PriceLockRefund,
    [Map("207")]
    DualInvestLiteSubscribe,
    [Map("208")]
    DualInvestLiteCollection,
    [Map("209")]
    DualInvestLiteProfit,
    [Map("210")]
    DualInvestLiteRefund,
    [Map("211")]
    SatoshiCryptoWin,
    [Map("212")]
    FlexLoanCollateralLocked,
    [Map("213")]
    FlexLoanCollateralTransferOut,
    [Map("214")]
    FlexLoanCollateralReturn,
    [Map("215")]
    FlexLoanCollateralRelease,
    [Map("216")]
    FlexLoanLoanTransferIn,
    [Map("217")]
    FlexLoanCollateralLoanBorrowed,
    [Map("218")]
    FlexLoanCollateralLoanRepaid,
    [Map("219")]
    FlexLoanLoanTransferOut,
    [Map("220")]
    Delisted,
    [Map("221")]
    BlockchainWithdrawalFee,
    [Map("222")]
    WithdrawalFeeRefund,
    [Map("223")]
    SwapLeadTradingProfitShare,
    [Map("266")]
    SpotLeadTradingProfitShare,
    [Map("224")]
    ServiceFee,
    [Map("225")]
    SharkFinSubscribe,
    [Map("226")]
    SharkFinCollection,
    [Map("227")]
    SharkFinProfit,
    [Map("228")]
    SharkFinRefund,
    [Map("229")]
    Airdrop,
    [Map("230")]
    TokenMigrationComplete,
    [Map("232")]
    SubsidizedInterestReceive,
    [Map("233")]
    BrokerRebateCompensation,
    [Map("249")]
    SeagullSubscribe,
    [Map("250")]
    SeagullCollection,
    [Map("251")]
    SeagullProfit,
    [Map("252")]
    SeagullRefund,
    [Map("263")]
    StrategyBotsProfitShare,
    [Map("270")]
    DcdBrokerTransfer,
    [Map("271")]
    DcdBrokerRebate,
    [Map("284")]
    TransferOutTradingSubAccount,
    [Map("285")]
    TransferInTradingSubAccount,
    [Map("286")]
    TransferOutCustodyFundingAccount,
    [Map("287")]
    TransferIntCustodyFundingAccount,
    [Map("288")]
    CustodyFundDelegation,
    [Map("289")]
    CustodyFundUndelegation,
    [Map("303")]
    SnowballMarketTransfer,
    [Map("304")]
    SimpleEarnFixedOrderSubmission,
    [Map("305")]
    SimpleEarnFixedOrderRedemption,
    [Map("306")]
    SimpleEarnFixedPrincipalDistribution,
    [Map("307")]
    SimpleEarnFixedInterestDistributionEarly,
    [Map("308")]
    SimpleEarnFixedInterestDistribution,
    [Map("309")]
    SimpleEarnFixedInterestDistributionExtension,
    [Map("311")]
    CryptoDustAutoTransferIn,
}