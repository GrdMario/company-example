namespace Company.Example.Domain
{
    using System;

    public class Car
    {
        public Car(
            Guid id,
            string name,
            string make,
            int weight,
            int maxSpeed)
        {
            this.Id = id;
            this.Name = name;
            this.Make = make;
            this.Weight = weight;
            this.MaxSpeed = maxSpeed;
        }

        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Make { get; protected set; }

        public int Weight { get; protected set; }

        public int MaxSpeed { get; protected set; }

        public void Update(string name, string make, int weight, int maxSpeed)
        {
            this.Name = name;
            this.Make = make;
            this.Weight = weight;
            this.MaxSpeed = maxSpeed;
        }
    }
}
