using dacsanvungmien.Dtos;
using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien
{
    public static class Extensions
    {
        public static CategoryDto AsDto(this Category cartegory)
        {
            return new CategoryDto
            {
                Id=cartegory.Id,
                Name = cartegory.Name
            };
        }
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                PriceSale = product.PriceSale,
                Dvt=product.Dvt,
                Weight=product.Weight,
                Amount=product.Amount,
                RegionId = product.RegionId,
                ProvinceId=product.ProvinceId,
            };
        }
        public static ProductImageDto AsDto(this ProductImage productImage, string imageSrc)
        {
            return new ProductImageDto
            {
                Id = productImage.Id,
                Image = productImage.Image,
                ImageSrc = imageSrc,
                ProductId = productImage.ProductId
            };
        }
        public static RegionDto AsDto(this Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Name = region.Name
            };
        }

        public static WeightDto AsDto(this Weight weight)
        {
            return new WeightDto
            {
                Id = weight.Id,
                Name = weight.Name
            };
        }

        public static DvtDto AsDto(this Dvt dvt)
        {
            return new DvtDto
            {
                Id = dvt.Id,
                Name = dvt.Name
            };
        }

        public static ProvinceDto AsDto(this Province province)
        {
            return new ProvinceDto
            {
                Id = province.Id,
                Name = province.Name,
                RegionId=province.RegionId,
            };
        }

        public static BillDto AsDto(this Bill bill)
        {
            return new BillDto
            {
                Id = bill.Id,
                Status = bill.Status,
                Total=bill.Total,
                OrderTime=bill.OrderTime,
                UserId=bill.UserId,
            };
        }
        public static CartDto AsDto(this Cart cart)
        {
            return new CartDto
            {
                Id=cart.Id,
                Amount=cart.Amount,
                BillId=cart.BillId,
                ProductId=cart.ProductId,
                Price=cart.Price,
            };
        }
        public static AccountDto AsDto(this Account account,string token )
        {
            return new AccountDto
            {
                Name=account.Name,
                Gmail=account.Gmail,
                UserAddress=account.UserAddress,
                FaceBook=account.FaceBook,
                PhoneNumber=account.PhoneNumber,
                Token=token
            };
        }
        public static UserDto AsDto(this Account account)
        {
            return new UserDto
            {
                Id=account.Id,
                AccountPassword=account.AccountPassword,
                Name = account.Name,
                UserAddress = account.UserAddress,
                FaceBook= account.FaceBook,
                Gmail= account.Gmail,
                PhoneNumber= account.PhoneNumber,
                Roles=account.Roles,
            };
        }
    }
}
