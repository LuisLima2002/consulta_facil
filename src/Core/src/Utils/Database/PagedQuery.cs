namespace VozAmiga.Api.Utils;

public record PagedQuery<TValue>(IEnumerable<TValue> Values, int Total);
