namespace ADO.NET_Crud.DatabaseConnectivity
{
    public static class Connection
    {
        private static string DataBaseConn = "Server=DESKTOP-L7D7AQ5\\SQLEXPRESS01;Database=ADO_ASP_Crud;User Id=hammad;Password=1234;Trusted_Connection=true";
        
        public static string MyConn { get => DataBaseConn; }
    }
}
