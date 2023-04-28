namespace BusinessLayer.Messages
{
    public static class Message
    {
        public const string ItemDoesNotExist = "Item given in the request does not exists.";
        public const string ItemAreNotGiven = "Items are not given in the request";
        public const string ItemListIsEmpty = "Items list provided in the request is empty.";
        public const string CustomerDoesNotExist = "Customer given in the request does not exists.";
        public const string CustomerWithEmailExistInSystem = "Customer email already exists in the system.";
        public const string PleaseProvideTheCustomerInTheRequest = "PleaseProvideTheCustomerInTheRequest.";
        public const string CustomerNameCannotBeNull = "Customer name cannot be null in the request.";
        public const string CustomerNameCannotBeEmpty = "Customer name cannot be empty in the request.";
        public const string CustomerEmailCannotBeNull = "Customer email cannot be null in the request.";
        public const string CustomerEmailCannotBeEmpty = "Customer email cannot be empty in the request.";
        public const string CustomerEmailIsNotAValidEmail = "Customer email is not a valid email address.";
    }
}
