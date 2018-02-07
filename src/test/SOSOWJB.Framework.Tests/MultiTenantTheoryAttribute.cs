using Xunit;

namespace SOSOWJB.Framework.Tests
{
    public sealed class MultiTenantTheoryAttribute : TheoryAttribute
    {
        public MultiTenantTheoryAttribute()
        {
            if (!FrameworkConsts.MultiTenancyEnabled)
            {
#pragma warning disable CS0162 // 检测到无法访问的代码
                Skip = "MultiTenancy is disabled.";
#pragma warning restore CS0162 // 检测到无法访问的代码
            }
        }
    }
}