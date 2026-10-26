namespace Findirector.Domain;

/// <summary>Результат прогноза; отрицательные деньги показывают дефицит.</summary>
public sealed record MonthReport(
    decimal Revenue,
    decimal VariableCosts,
    decimal FixedCosts,
    decimal CashReceipts,
    decimal ClosingCash,
    decimal ClosingReceivables)
{
    public decimal Expenses => VariableCosts + FixedCosts;
    public decimal MarginalIncome => Revenue - VariableCosts;
    public decimal Profit => Revenue - Expenses;
    public decimal FundingGap => Math.Max(0m, -ClosingCash);
}