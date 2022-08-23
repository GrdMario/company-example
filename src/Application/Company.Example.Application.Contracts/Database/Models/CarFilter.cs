namespace Company.Example.Application.Contracts.Database.Models
{
    public class CarFilter
    {
        public CarFilter(
            int skip,
            int take,
            string? name,
            string? make)
        {
            this.Skip = skip;
            this.Take = take;
            this.Name = name;
            this.Make = make;
        }

        public int Skip { get; set; }

        public int Take { get; set; }

        public string? Name { get; set; }

        public string? Make { get; set; }
    }
}
