using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Shared.Interfaces;

namespace Metalama.Shared;

public class InjectionAttribute : TypeAspect
{
    /// <inheritdoc />
    public override void BuildAspect(IAspectBuilder<INamedType> builder)
    {
        var genericType = builder.Target.TypeParameters[0];

        var listType = ((INamedType)TypeFactory.GetType(typeof(List<>))).WithTypeArguments(genericType);
        var entities = builder.Advice.IntroduceField(builder.Target, "_entities", listType,
            IntroductionScope.Instance, OverrideStrategy.Ignore,
            fieldBuilder =>
            {
                fieldBuilder.InitializerExpression = ExpressionFactory.Parse("new()");
                fieldBuilder.Accessibility = Accessibility.Private;
            });

        builder.Advice.IntroduceMethod(
            builder.Target,
            nameof(Add),
            IntroductionScope.Instance,
            OverrideStrategy.Ignore,
            methodBuilder =>
            {
                methodBuilder.Accessibility = Accessibility.Public;
            },
            new { T = genericType, entitiesField = entities.Declaration });

        builder.Outbound.SelectMany(type => type.Methods)
            .AddAspectIfEligible<LogAttribute>();
    }

    [Template]
    private void Add<[CompileTime] T>(T data, IField entitiesField) where T : IHasId
    {
        var entities = (List<T>)entitiesField.Value;
        entities.Add(data);
    }
}