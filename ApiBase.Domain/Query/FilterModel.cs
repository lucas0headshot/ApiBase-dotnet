using ApiBase.Domain.Enums;

namespace ApiBase.Domain.Query
{
    public class FilterModel
    {
        public const string OpEquals = "equal";
        public const string OpGreater = "greater";
        public const string OpLess = "less";
        public const string OpContains = "contains";
        public const string OpContainsAll = "containsall";
        public const string OpIn = "in";
        public const string OpGreaterOrEqual = "greaterOrEqual";
        public const string OpLessOrEqual = "lessOrEqual";
        public const string OpStartsWith = "startswith";
        public const string OpEndsWith = "endswith";
        public const string OpInOrNull = "inOrNull";

        public string property { get; set; }
        public object value { get; set; }
        public string @operator { get; set; } = OpEquals;
        public FilterOperator? Operator { get; set; }
        public bool And { get; set; } = true;
        public bool Not { get; set; } = false;
        public bool MainFilter { get; set; }

        public string PropertyName
        {
            get
            {
                var parts = (property ?? string.Empty).Split('.');
                return parts.Last();
            }
        }

        public FilterModel() { }

        public FilterOperator GetOperator()
        {
            if (Operator.HasValue)
                return Operator.Value;

            return @operator switch
            {
                OpEquals => FilterOperator.Equal,
                OpGreater => FilterOperator.GreaterThan,
                OpLess => FilterOperator.LessThan,
                OpContains => FilterOperator.Contains,
                OpContainsAll => FilterOperator.ContainsAll,
                OpIn => FilterOperator.In,
                OpGreaterOrEqual => FilterOperator.GreaterThanOrEqual,
                OpLessOrEqual => FilterOperator.LessThanOrEqual,
                OpStartsWith => FilterOperator.StartsWith,
                OpEndsWith => FilterOperator.EndsWith,
                OpInOrNull => FilterOperator.InOrNull,
                _ => throw new Exception($"Unknown operator '{@operator}'"),
            };
        }
    }
}
