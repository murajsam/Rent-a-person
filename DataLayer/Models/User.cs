namespace DataLayer.Models
{
    public class User
    {
        private int userId;

        public int UserId 
        { 
            get { return userId; }
            set { userId = value; }
        }

        private Person userDetails;

        public Person UserDetails
        {
            get { return userDetails; }
            set { userDetails = value; }
        }

        public User() { }
        public User(int userId, Person userDetails)
        {
            this.userId = userId;
            this.userDetails = userDetails;
        }

    }
}
