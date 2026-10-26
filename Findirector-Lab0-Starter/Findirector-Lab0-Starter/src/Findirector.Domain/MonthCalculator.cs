namespace Findirector.Domain;

public sealed class MonthCalculator
{
    public MonthReport Calculate(MonthPlan plan, decimal openingCash, decimal openingReceivables)
    {
        ArgumentNullException.ThrowIfNull(plan);
        // Прогнозный минус переносится из предыдущего месяца, а не обнуляется.
        Money.Check(openingCash, nameof(openingCash), allowNegative: true);
        Money.Check(openingReceivables, nameof(openingReceivables));

        decimal revenue = plan.Quantity * plan.Price;                         // П-Б1
        decimal variableCosts = plan.Quantity * plan.UnitCost;
        decimal paidNow = decimal.Round(revenue * plan.PaidNowPercent / 100m,
            2, MidpointRounding.AwayFromZero);                                // П-Б3, П-Б9
        decimal receipts = openingReceivables + paidNow;                      // П-Б4
        decimal closingCash = openingCash + receipts - variableCosts - plan.FixedCosts;
        decimal receivables = revenue - paidNow;                              // П-Б3
	

        return new MonthReport(revenue, variableCosts, plan.FixedCosts,
            receipts, closingCash, receivables);
    }
}
