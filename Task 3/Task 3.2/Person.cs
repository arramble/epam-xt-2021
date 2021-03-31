namespace CustomTypes
{
    public class Person
    {
        public string Name { get; }

        public Person(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if ((this == null) || (obj == null))
            {
                return Equals(this, obj);
            }
            else if (obj.GetType() != this.GetType())
            {
                return false;
            }
            else
            {
                return this.Name == (obj as Person).Name;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
