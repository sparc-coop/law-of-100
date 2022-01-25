using Sparc.Features;

namespace LawOf100.Features.Features
{
    public record UppercaseTextResponse(string Result);
    public class UppercaseText : PublicFeature<string, UppercaseTextResponse>
    {
        public async override Task<UppercaseTextResponse> ExecuteAsync(string input)
        {
            return new(input.ToUpper());
        }
    }
}
