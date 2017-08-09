namespace App.Security.Authorization.Infrastructure
{
    interface IBinarySerializer
    {
        object Deserialize(byte[] value);
        byte[] Serialize(object value);
    }
}