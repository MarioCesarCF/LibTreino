namespace LibTreino.Data
{
    public class ConfigDatabaseSettings
    {
        //Verificar como fazer com mais de uma collection no mesmo banco
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string ShoppingListCollectionName { get; set; }
    }
}
