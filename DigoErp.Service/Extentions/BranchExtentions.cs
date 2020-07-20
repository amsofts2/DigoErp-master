using DigoErp.Repository.Edmx;

namespace DigoErp.Service.Extentions
{
    public static class BranchExtentions
    {

        public static Models.Branch MapFrom(this Tbl_Branch branch)
        {
            return new Models.Branch
            {
                Id = branch.Id,
                Name = branch.Name,
                Email = branch.Email,
                Phone = branch.Phone,
                TaxNumber = branch.TaxNumber,
                Address = branch.Address,
                Logo = branch.Logo
            };
        }

        public static Tbl_Branch MapFrom(this Models.Branch branch)
        {
            return new Tbl_Branch
            {
                Id = branch.Id,
                Name = branch.Name,
                Email = branch.Email,
                Phone = branch.Phone,
                TaxNumber = branch.TaxNumber,
                Address = branch.Address,
                Logo = branch.Logo
            };
        }
    }
}
