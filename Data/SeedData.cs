using Core.Helpers;
using Domain.Entities;
using Domain.Settings;

namespace Data
{
    public class SeedData
    {
        public static async Task Execute(DataContext context, DefaultCredential defaultCredential)
        {
            if (!context.Users!.Any() && !context.Roles!.Any())
            {
                await SeedDefaultCredential(context, defaultCredential);
            }

            if (!context.Brands!.Any() && !context.Categories!.Any() &&
                !context.Conditions!.Any() && !context.ProductTypes!.Any() &&
                !context.SubBrands!.Any() && !context.Products!.Any())
            {
                await SeedSampleProducts(context, defaultCredential);
            }
        }

        private static async Task SeedDefaultCredential(DataContext context, DefaultCredential defaultCredential)
        {
            var passwordHandleResult = defaultCredential.DefaultPassword?.HashPassword();

            var roles = new List<Role>
                {
                    new Role
                    {
                        Id = defaultCredential.AdminRoleId,
                        Name = Domain.Enums.Role.Admin.ToString(),
                        Description = Domain.Enums.Role.Admin.ToString()
                    },
                    new Role
                    {
                        Name = Domain.Enums.Role.Moderator.ToString(),
                        Description = Domain.Enums.Role.Moderator.ToString()
                    },
                    new Role
                    {
                        Name = Domain.Enums.Role.Staff.ToString(),
                        Description = Domain.Enums.Role.Staff.ToString()
                    },
                    new Role
                    {
                        Name = Domain.Enums.Role.Basic.ToString(),
                        Description = Domain.Enums.Role.Basic.ToString()
                    }
                };

            await context.Roles!.AddRangeAsync(roles);

            var defaultUser = new User
            {
                Id = defaultCredential.AdminId,
                Email = "trungthuongvo109@gmail.com",
                PhoneNumber = "037 527 4267",
                FullName = "Võ Trung Thường",
                Avatar = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638705794/TGProV3/users/admin_avatar.jpg",
                AvatarId = "TGProV3/users/admin_avatar",
                PasswordHash = passwordHandleResult?.PasswordHash,
                PasswordSalt = passwordHandleResult?.PasswordSalt,
                IsBlocked = false,
                Gender = Domain.Enums.Gender.Male
            };

            defaultUser.UserRoles?.Add(new UserRole
            {
                UserId = defaultCredential.AdminId,
                RoleId = defaultCredential.AdminRoleId
            });

            await context.Users!.AddAsync(defaultUser);
            await context.SaveChangesAsync();
        }
        private static async Task SeedSampleProducts(DataContext context, DefaultCredential defaultCredential)
        {
            Guid dellId = Guid.NewGuid();
            Guid lenovoId = Guid.NewGuid();
            Guid asusId = Guid.NewGuid();
            Guid msiId = Guid.NewGuid();
            Guid hpId = Guid.NewGuid();
            Guid appleId = Guid.NewGuid();
            Guid intelId = Guid.NewGuid();
            Guid lgId = Guid.NewGuid();
            Guid razerId = Guid.NewGuid();
            Guid microsoftId = Guid.NewGuid();

            Guid laptopId = Guid.NewGuid();
            Guid pcId = Guid.NewGuid();
            Guid accessoriesId = Guid.NewGuid();

            Guid sealedId = Guid.NewGuid();
            Guid fullboxId = Guid.NewGuid();
            Guid outletId = Guid.NewGuid();
            Guid usedId = Guid.NewGuid();

            Guid genuineId = Guid.NewGuid();
            Guid importId = Guid.NewGuid();

            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = dellId,
                    Name = "Dell",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/dell.png",
                    LogoId = "TGProV3/brands/dell",
                    Description = "Dell là thương hiệu có quá trình phát triển lâu dài và bền bỉ trong ngành công nghiệp máy tính. Dell cung cấp nhiều dòng laptop chất lượng, cao cấp như XPS, Precision, Latitude và nổi bật với G-Series Gaming và Alienware hàng đầu dành cho game thủ.",
                    CreatedBy = defaultCredential.AdminId,
                },
                new Brand
                {
                    Id = lenovoId,
                    Name = "Lenovo",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/lenovo.png",
                    LogoId = "TGProV3/brands/lenovo",
                    Description = "Lenovo đặc biệt thành công với dòng laptop doanh nhân cao cấp ThinkPad lâu đời và mở rộng các dòng sản phẩm mới mang tính sáng tạo IdeaPad, Legion dành cho gaming và ThinkBook nhắm tới đối tượng học sinh, sinh viên.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = asusId,
                    Name = "Asus",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/asus.png",
                    LogoId = "TGProV3/brands/asus",
                    Description = "ASUS là thương hiệu laptop số 1 thế giới ở thời điểm hiện tại. Dòng sản phẩm đa dạng, phục vụ nhiều nhu cầu và luôn mang một chất riêng giúp ASUS ghi điểm với người dùng toàn cầu.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = msiId,
                    Name = "MSI",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/msi.png",
                    LogoId = "TGProV3/brands/msi",
                    Description = "MSI (tên đầy đủ: Micro-Star International Co., Ltd) - Một trong những hãng sản xuất Laptop gaming lớn nhất Đài Loan. MSI là doanh nghiệp công nghệ toàn cầu và uy tín trên thế giới.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = hpId,
                    Name = "HP",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/hp.png",
                    LogoId = "TGProV3/brands/hp",
                    Description = "HP sản xuất máy tính xách tay phục vụ mọi nhu cầu bao gồm cả cho gia đình và văn phòng tại nhà, công ty và doanh nghiệp. Đồng thời HP cung cấp đa dạng nhu cầu với dải sản phẩm rất rộng bao gồm EliteBook, ZBook, Envy, Spectre, Pavilion, EliteDesk.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = appleId,
                    Name = "Apple",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/apple.png",
                    LogoId = "TGProV3/brands/apple",
                    Description = "Apple thành công khi đưa ra một hệ sinh thái riêng sử dụng nền tảng hệ điều hành do chính hãng phát triển. Các dòng laptop, PC của hãng đều có độ ổn định, hiệu năng và thời lượng pin vượt trội so với phần còn lại.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = intelId,
                    Name = "Intel",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/intel.png",
                    LogoId = "TGProV3/brands/intel",
                    Description = "Tập đoàn Intel (Integrated Electronics) thành lập năm 1968 tại Santa Clara, California, Hoa Kỳ, là nhà sản xuất các sản phẩm như chip vi xử lý cho máy tính, bo mạch chủ, ổ nhớ flash, card mạng và các thiết bị máy tính khác.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = lgId,
                    Name = "LG",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/lg.png",
                    LogoId = "TGProV3/brands/lg",
                    Description = "Máy tính xách tay LG thế hệ mới mang vẻ đẹp cao cấp cùng nhiều tính năng đáng giá, mang thiết kế mỏng nhẹ, tinh tế cùng cấu hình cao. Laptop LG chính hãng phù hợp với nhu cầu học tập, giải trí và tạo ra những trải nghiệm thú vị cho người dùng.",
                    CreatedBy = defaultCredential.AdminId
                },
                new Brand
                {
                    Id = razerId,
                    Name = "Razer",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/razer.png",
                    LogoId = "TGProV3/brands/razer",
                    Description = "Razer là hãng sản xuất thiết bị Gaming nổi tiếng với sự cao cấp và thiết kế hàng đầu. Đặc biệt, Laptop Gaming Razer Blade luôn mang lại những sự tinh tế và hiệu suất khác biệt.",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Brand
                {
                    Id = microsoftId,
                    Name = "Microsoft",
                    LogoUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638721714/TGProV3/brands/microsoft.png",
                    LogoId = "TGProV3/brands/microsoft",
                    Description = "Microsoft vốn là công ty phần mềm đứng đầu thế giới với hệ điều hành Windows phổ biến. Trong vài năm trở lại đây, Microsoft đang tham gia vào thị trường máy tính với dòng sản phẩm Surface và ngay lập tức được người dùng đón nhận.",
                    CreatedBy =  defaultCredential.AdminId
                }
            };

            var categories = new List<Category>
            {
                new Category
                {
                    Id = laptopId,
                    Name = "Laptop",
                    Description = "Máy tính xách tay",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Category
                {
                    Id = pcId,
                    Name = "PC",
                    Description = "Máy tính để bàn (đồng bộ)",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Category
                {
                    Id = accessoriesId,
                    Name = "Accessories",
                    Description = "Phụ kiện",
                    CreatedBy =  defaultCredential.AdminId
                }
            };

            var conditions = new List<Condition>
            {
                new Condition
                {
                    Id = sealedId,
                    Name = "Hàng New Sealed",
                    Description = "Hàng nguyên niêm phong của nhà sản xuất, chưa mở. Tình trạng mới 100%.",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Condition
                {
                    Id = fullboxId,
                    Name = "Hàng New Fullbox",
                    Description = "Hàng nguyên thùng của nhà sản xuất, được mở seal hộp để kiểm tra hàng hoá hải quan hoặc dán tem niêm phong của nhà bán lẻ, phân phối.",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Condition
                {
                    Id = outletId,
                    Name = "Hàng New Outlet",
                    Description = "Hàng giảm giá của nhà sản xuất do tồn kho lâu hoặc các đơn hàng bị huỷ, trả bởi khách hàng, tình trạng mới 100% chưa qua sử dụng.",
                    CreatedBy =  defaultCredential.AdminId
                },
                new Condition
                {
                    Id = usedId,
                    Name = "Hàng qua sử dụng",
                    Description = "Hàng qua sử dụng nhập lại từ người dùng, đã được kiểm định chất lượng bởi đội ngũ kỹ thuật.",
                    CreatedBy =  defaultCredential.AdminId
                },
            };

            var productTypes = new List<ProductType>
            {
                new ProductType
                {
                    Id = genuineId,
                    Name = "Chính hãng",
                    Description = "Phân phối chính hãng bởi TGPro.",
                    CreatedBy =  defaultCredential.AdminId
                },
                new ProductType
                {
                    Id = importId,
                    Name = "Nhập khẩu",
                    Description = "Phân phối bởi TGPro.",
                    CreatedBy =  defaultCredential.AdminId
                },
            };

            await context.Brands!.AddRangeAsync(brands);
            await context.Categories!.AddRangeAsync(categories);
            await context.Conditions!.AddRangeAsync(conditions);
            await context.ProductTypes!.AddRangeAsync(productTypes);

            var subBrands = new List<SubBrand>
            {
                new SubBrand
                {
                    Name = "ThinkPad",
                    BrandId = lenovoId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "IdeaPad",
                    BrandId = lenovoId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Legion",
                    BrandId = lenovoId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ThinkBook",
                    BrandId = lenovoId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ThinkCenter",
                    BrandId = lenovoId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Legion",
                    BrandId = lenovoId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ThinkStation",
                    BrandId = lenovoId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Inspiron",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Vostro",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "XPS",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "G-Gaming Series",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Alienware",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Latitute",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Precision",
                    BrandId = dellId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Optiplex",
                    BrandId = dellId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Precision",
                    BrandId = dellId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Alienware",
                    BrandId = dellId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Elitebook",
                    BrandId = dellId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Zbook",
                    BrandId = hpId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Envy",
                    BrandId = hpId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Omen",
                    BrandId = hpId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Pavilion",
                    BrandId = hpId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Elitedesk",
                    BrandId = hpId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Pavilion",
                    BrandId = hpId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Z Workstation",
                    BrandId = hpId,
                    CategoryId = pcId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ExpertBook",
                    BrandId = asusId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "VivoBook",
                    BrandId = asusId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ZenBook",
                    BrandId = asusId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "TUF/ROG Gaming",
                    BrandId = asusId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "ProArt Studio",
                    BrandId = asusId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Surface Laptop",
                    BrandId = microsoftId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Surface Book",
                    BrandId = microsoftId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
                new SubBrand
                {
                    Name = "Surface Pro",
                    BrandId = microsoftId,
                    CategoryId = laptopId,
                    CreatedBy =  defaultCredential.AdminId
                },
            };

            await context.AddRangeAsync(subBrands);

            await context.SaveChangesAsync();

            var products = new List<Product>
            {
                new Product
                {
                    SerialNumber = "Omen152020A01NU",
                    Name = "HP Omen 15 2020 (AMD)",
                    CategoryId = laptopId,
                    ConditionId = sealedId,
                    SubBrandId = context.SubBrands!.FirstOrDefault(x=>x.Name == "Omen")!.Id,
                    ProductTypeId = importId,
                    Specification = "Dng cap nhat...",
                    Description = "Dang cap nhat...",
                    Warranty = "Dang cap nhat...",
                    Price = 27490000,
                    UnitsInStock = 10,
                    UnitsOnOrder = 0,
                    Discontinued = false,
                    CreatedBy =  defaultCredential.AdminId
                },
                new Product
                {
                    SerialNumber = "ZenbookQ407iq01NS",
                    Name = "Asus ZenBook 14 Q407iq",
                    CategoryId = laptopId,
                    ConditionId = fullboxId,
                    SubBrandId = context.SubBrands!.FirstOrDefault(x=>x.Name == "Zenbook")!.Id,
                    ProductTypeId = genuineId,
                    Specification = "Dng cap nhat...",
                    Description = "Dang cap nhat...",
                    Warranty = "Dang cap nhat...",
                    Price = 18490000,
                    UnitsInStock = 15,
                    UnitsOnOrder = 0,
                    Discontinued = false,
                    CreatedBy =  defaultCredential.AdminId
                },
                new Product
                {
                    SerialNumber = "X1Nano02NF",
                    Name = "Lenovo ThinkPad X1 Nano",
                    CategoryId = laptopId,
                    ConditionId = sealedId,
                    SubBrandId = context.SubBrands!.FirstOrDefault(x=>x.Name == "ThinkPad")!.Id,
                    ProductTypeId = importId,
                    Specification = "Dng cap nhat...",
                    Description = "Dang cap nhat...",
                    Warranty = "Dang cap nhat...",
                    Price = 37990000,
                    UnitsInStock = 10,
                    UnitsOnOrder = 0,
                    Discontinued = false,
                    CreatedBy =  defaultCredential.AdminId
                },
                new Product
                {
                    SerialNumber = "DellG15551001NS",
                    Name = "Dell Gaming G15 5510",
                    CategoryId = laptopId,
                    ConditionId = fullboxId,
                    SubBrandId = context.SubBrands!.FirstOrDefault(x=>x.Name == "ThinkPad")!.Id,
                    ProductTypeId = genuineId,
                    Specification = "Dng cap nhat...",
                    Description = "Dang cap nhat...",
                    Warranty = "Dang cap nhat...",
                    Price = 23990000,
                    UnitsInStock = 20,
                    UnitsOnOrder = 0,
                    Discontinued = false,
                    CreatedBy =  defaultCredential.AdminId
                },
                new Product
                {
                    SerialNumber = "SurfaceLaptop313506NO",
                    Name = "Surface Laptop 3 13.5inch",
                    CategoryId = laptopId,
                    ConditionId = sealedId,
                    SubBrandId = context.SubBrands!.FirstOrDefault(x=>x.Name == "G-Gaming Series")!.Id,
                    ProductTypeId = importId,
                    Specification = "Dng cap nhat...",
                    Description = "Dang cap nhat...",
                    Warranty = "Dang cap nhat...",
                    Price = 21990000,
                    UnitsInStock = 12,
                    UnitsOnOrder = 0,
                    Discontinued = false,
                    CreatedBy =  defaultCredential.AdminId
                },
            };

            await context.Products!.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }
}
