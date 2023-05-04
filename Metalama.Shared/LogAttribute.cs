using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using Microsoft.Extensions.Logging;

namespace Metalama.Shared;

public class LogAttribute : OverrideMethodAspect
{
    [IntroduceDependency]
    private readonly ILogger _logger;

    /// <summary>Default template of the new method implementation.</summary>
    /// <returns></returns>
    public override dynamic? OverrideMethod()
    {
        try
        {
            _logger.LogTrace($"{meta.Target.Type.ToDisplayString(CodeDisplayFormat.MinimallyQualified)} started.");

            var result = meta.Proceed();

            _logger.LogTrace($"{meta.Target.Type.ToDisplayString(CodeDisplayFormat.MinimallyQualified)} successfully completed.");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown.");
            throw;
        }
    }

    /// <inheritdoc />
    public override void BuildEligibility(IEligibilityBuilder<IMethod> builder)
    {
        base.BuildEligibility(builder);
        builder.AddRule(EligibilityRuleFactory.GetAdviceEligibilityRule(AdviceKind.OverrideMethod));
    }
}