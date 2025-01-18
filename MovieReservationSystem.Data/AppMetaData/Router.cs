namespace MovieReservationSystem.Data.AppMetaData
{
    public static class Router
    {
        public const string Root = "Api/";
        public const string Version = "V1/";
        public const string Rule = Root + Version;

        public const string SingleRoute = "{id}";


        public static class MovieRouting
        {
            public const string Prefix = Rule + "Movie/";

            public const string list = Prefix + "List";
            public const string PaginatedList = Prefix + "PaginatedList";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class GenreRouting
        {
            public const string Prefix = Rule + "Genre/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class SeatTypeRouting
        {
            public const string Prefix = Rule + "SeatType/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class HallRouting
        {
            public const string Prefix = Rule + "Hall/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class SeatRouting
        {
            public const string Prefix = Rule + "Seat/";

            public const string list = Prefix + "List";
            public const string FreeSeatsInShowTime = Prefix + "FreeSeats/{showTimeId}";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class ActorRouting
        {
            public const string Prefix = Rule + "Actor/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class DirectorRouting
        {
            public const string Prefix = Rule + "Director/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class ShowTimeRouting
        {
            public const string Prefix = Rule + "ShowTime/";

            public const string list = Prefix + "List";
            public const string comingList = Prefix + "ComingList";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class UserRouting
        {
            public const string Prefix = Rule + "User/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string ChangePassword = Prefix + "ChangePassword";
            public const string Delete = Prefix + SingleRoute;
            public const string ConfirmEmail = "Api/V1/User/ConfirmEmail";
            public const string RequestPasswordReset = Prefix + "RequestPasswordReset";
            public const string ValidatePasswordResetCode = Prefix + "ValidatePasswordResetCode";
            public const string ResetPassword = Prefix + "ResetPassword";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication/";

            public const string ValidateToken = Prefix + "ValidateToken";

            public const string SignIn = Prefix + "SignIn";

            public const string RefreshToken = Prefix + "RefreshToken";
        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization/";

            public const string CreateRole = Prefix + "Role/Create";
            public const string EditRole = Prefix + "Role/Edit";
            public const string DeleteRole = Prefix + "Role/" + SingleRoute;

            public const string list = Prefix + "Role/" + "List";
            public const string GetById = Prefix + "Role/" + SingleRoute;
            public const string GetUserRoles = Prefix + "Role/User-Roles" + SingleRoute;
            public const string EditUserRoles = Prefix + "Role/Edit-User-Roles";

        }
        public static class ReservationRouting
        {
            public const string Prefix = Rule + "Reservation/";

            public const string list = Prefix + "List";
            public const string PaginatedList = Prefix + "PaginatedList";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class PaymentRouting
        {
            public const string Prefix = Rule + "Payment/";

            public const string Create = Prefix + "{reservationId}";
            public const string webhook = Prefix + "webhook";
        }
    }
}
