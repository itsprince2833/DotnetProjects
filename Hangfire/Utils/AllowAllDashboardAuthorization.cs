namespace Hangfire.Utils;

public class AllowAllDashboardAuthorization : Hangfire.Dashboard.IDashboardAuthorizationFilter
{
    public bool Authorize(Hangfire.Dashboard.DashboardContext context) => true;
}
