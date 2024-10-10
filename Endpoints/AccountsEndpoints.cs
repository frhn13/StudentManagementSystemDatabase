namespace StudentDatabaseServer.Endpoints
{
    public static class AccountsEndpoints
    {
        const string GetAccountEndpointName = "GetAccount";
        public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("accounts").WithParameterValidation(); // All endpoints of URL start with "accounts"
            return group;
        }
    }
}
