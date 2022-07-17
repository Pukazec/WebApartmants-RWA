using ApartmantsLibrary.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Dal
{
    public class DBRepo : IRepo
    {
        private static readonly string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //-------------------------------------------------------------------------------- Authentication --------------------------------------------------------------------------------
        public User AuthenticateUser(string username, string password)
        {
            var tblAuthenticate = SqlHelper.ExecuteDataset(CS, nameof(AuthenticateUser), username, password).Tables[0];
            if (tblAuthenticate.Rows.Count == 0) return null;

            DataRow row = tblAuthenticate.Rows[0];
            return new User
            {
                IdUser = (int)row[nameof(User.IdUser)],
                Id = row[nameof(User.Id)].ToString(),
                Guid = (Guid)row[nameof(User.Guid)],
                UserName = row[nameof(User.UserName)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString()),
                Password = row[nameof(User.Password)].ToString()
            };
        }

        //-------------------------------------------------------------------------------- Load lists --------------------------------------------------------------------------------

        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartmants = SqlHelper.ExecuteDataset(CS, nameof(LoadApartments)).Tables[0];
            foreach (DataRow row in tblApartmants.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Guid = (Guid)row[nameof(Apartment.Guid)],
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        City = LoadCity((int)row[nameof(Apartment.CityId)]),
                        CityName = row[nameof(Apartment.CityName)].ToString(),
                        CityId = (int)row[nameof(Apartment.CityId)],
                        Owner = LoadOwner((int)row[nameof(Apartment.OwnerId)]),
                        OwnerName = row[nameof(Apartment.OwnerName)].ToString(),
                        OwnerId = (int)row[nameof(Apartment.OwnerId)],
                        Status = LoadStatus((int)row[nameof(Apartment.StatusId)]),
                        StatusName = row[nameof(Apartment.StatusNameEng)].ToString(),
                        StatusNameEng = row[nameof(Apartment.StatusNameEng)].ToString(),
                        StatusId = (int)row[nameof(Apartment.StatusId)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        NumOfPictures = (int)row[nameof(Apartment.NumOfPictures)],
                        Price = (decimal)row[nameof(Apartment.Price)],
                        Stars = LoadRating((int)row[nameof(Apartment.Id)]),
                        ImgUrl = row[nameof(Apartment.ImgUrl)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString())
                    });
            }

            return apartments;
        }

        public IList<Apartment> LoadFilteredApartments(string rooms, string adults, string children, string cityId)
        {
            IList<Apartment> apartments = new List<Apartment>();

            if (int.Parse(rooms) <= 0)
            {
                rooms = null;
            }
            if (int.Parse(adults) <= 0)
            {
                adults = null;
            }
            if (int.Parse(children) <= 0)
            {
                children = null;
            }
            if (cityId == "")
            {
                cityId = null;
            }


            var tblApartmants = SqlHelper.ExecuteDataset(CS, nameof(LoadFilteredApartments), rooms, adults, children, cityId).Tables[0];
            foreach (DataRow row in tblApartmants.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Guid = (Guid)row[nameof(Apartment.Guid)],
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        City = LoadCity((int)row[nameof(Apartment.CityId)]),
                        CityName = row[nameof(Apartment.CityName)].ToString(),
                        CityId = (int)row[nameof(Apartment.CityId)],
                        StatusId = (int)row[nameof(Apartment.StatusId)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        Price = (decimal)row[nameof(Apartment.Price)],
                        Stars = LoadRating((int)row[nameof(Apartment.Id)]),
                        ImgUrl = row[nameof(Apartment.ImgUrl)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString())
                    });
            }

            return apartments;
        }

        private int LoadRating(int apartmantId)
        {
            var tblRatings = SqlHelper.ExecuteDataset(CS, nameof(LoadRating), apartmantId).Tables[0];
            int rating = 0;
            int devide = 0;

            foreach (DataRow row in tblRatings.Rows)
            {
                rating += (int)row[nameof(Apartment.Stars)];
                devide++;
            }

            if (devide == 0)
            {
                return 0;
            }
            return rating / devide;
        }

        public IList<City> LoadCities()
        {
            IList<City> cities = new List<City>();

            var tblCities = SqlHelper.ExecuteDataset(CS, nameof(LoadCities)).Tables[0];
            foreach (DataRow row in tblCities.Rows)
            {
                cities.Add(
                    new City
                    {
                        Id = (int)row[nameof(City.Id)],
                        Guid = (Guid)row[nameof(City.Guid)],
                        Name = row[nameof(City.Name)].ToString(),
                    });
            }


            return cities;
        }

        public IList<Status> LoadStatuses()
        {
            IList<Status> statuses = new List<Status>();

            var tblStatuses = SqlHelper.ExecuteDataset(CS, nameof(LoadStatuses)).Tables[0];
            foreach (DataRow row in tblStatuses.Rows)
            {
                statuses.Add(
                    new Status
                    {
                        Id = (int)row[nameof(Status.Id)],
                        Guid = (Guid)row[nameof(Status.Guid)],
                        Name = row[nameof(Status.Name)].ToString(),
                        NameEng = row[nameof(Status.NameEng)].ToString(),
                    });
            }

            return statuses;
        }

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(LoadTags)).Tables[0];

            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Guid = (Guid)row[nameof(Tag.Guid)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString(),
                        TagType = LoadTagType((int)row[nameof(Tag.TypeId)]),
                        TaggedApartmants = (int)row[nameof(Tag.TaggedApartmants)],
                        CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString())
                    }); ;
            }

            return tags;
        }
        public IList<TagType> LoadTagTypes()
        {
            IList<TagType> tagTypes = new List<TagType>();

            var tblTagTypes = SqlHelper.ExecuteDataset(CS, nameof(LoadTagTypes)).Tables[0];

            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(
                    new TagType
                    {
                        Id = (int)row[nameof(TagType.Id)],
                        Guid = (Guid)row[nameof(TagType.Guid)],
                        Name = row[nameof(TagType.Name)].ToString(),
                        NameEng = row[nameof(TagType.NameEng)].ToString()
                    });
            }

            return tagTypes;
        }
        public IList<Owner> LoadOwners()
        {
            IList<Owner> owners = new List<Owner>();
            var tblOwners = SqlHelper.ExecuteDataset(CS, nameof(LoadOwners)).Tables[0];

            foreach (DataRow row in tblOwners.Rows)
            {
                owners.Add(
                    new Owner
                    {
                        Id = (int)row[nameof(Owner.Id)],
                        Guid = (Guid)row[nameof(Owner.Guid)],
                        Name = row[nameof(Owner.Name)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(Owner.CreatedAt)].ToString())
                    });
            }

            return owners;
        }

        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, nameof(LoadUsers)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        IdUser = (int)row[nameof(User.IdUser)],
                        Id = row[nameof(User.Id)].ToString(),
                        Guid = (Guid)row[nameof(User.Guid)],
                        UserName = row[nameof(User.UserName)].ToString(),
                        Address = row[nameof(User.Address)].ToString(),
                        PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                        Email = row[nameof(User.Email)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString()),
                        Password = row[nameof(User.Password)].ToString()
                    });
            }

            return users;
        }

        public IList<ApartmantPicture> LoadPictures(int apartmantId)
        {
            IList<ApartmantPicture> pictures = new List<ApartmantPicture>();

            var tblPictures = SqlHelper.ExecuteDataset(CS, nameof(LoadPictures), apartmantId).Tables[0];

            foreach (DataRow row in tblPictures.Rows)
            {
                pictures.Add(
                    new ApartmantPicture
                    {
                        Id = (int)row[nameof(ApartmantPicture.Id)],
                        ApartmentId = (int)row[nameof(ApartmantPicture.ApartmentId)],
                        Guid = (Guid)row[nameof(ApartmantPicture.Guid)],
                        CreatedAt = DateTime.Parse(row[nameof(ApartmantPicture.CreatedAt)].ToString()),
                        Bytes = row[nameof(ApartmantPicture.Bytes)].ToString(),
                        Name = row[nameof(ApartmantPicture.Name)].ToString(),
                        IsRepresentative = (bool)row[nameof(ApartmantPicture.IsRepresentative)]
                    });
            }

            return pictures;
        }

        public IList<TaggedApartmant> LoadTaggedApartmant(int apartmantId)
        {
            IList<TaggedApartmant> taggedApartmants = new List<TaggedApartmant>();

            var tblTaggedApartmants = SqlHelper.ExecuteDataset(CS, nameof(LoadTaggedApartmant), apartmantId).Tables[0];

            foreach (DataRow row in tblTaggedApartmants.Rows)
            {
                taggedApartmants.Add(
                    new TaggedApartmant
                    {
                        Id = (int)row[nameof(TaggedApartmant.Id)],
                        Guid = (Guid)row[nameof(TaggedApartmant.Guid)],
                        ApartmantId = apartmantId,
                        TagId = (int)row[nameof(TaggedApartmant.TagId)],
                        Apartment = LoadApartment(apartmantId),
                        Tag = LoadTag((int)row[nameof(TaggedApartmant.TagId)])
                    });
            }

            return taggedApartmants;
        }

        public IList<ApartmantReservation> LoadApartmantReservationE(int apartmantId)
        {
            IList<ApartmantReservation> apartmantReservations = new List<ApartmantReservation>();

            var tblReservation = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmantReservationE), apartmantId).Tables[0];

            foreach (DataRow row in tblReservation.Rows)
            {
                apartmantReservations.Add(
                    new ApartmantReservation
                    {
                        Id = (int)row[nameof(ApartmantReservation.Id)],
                        Guid = (Guid)row[nameof(ApartmantReservation.Guid)],
                        ApartmentId = (int)row[nameof(ApartmantReservation.ApartmentId)],
                        Apartment = LoadApartment((int)row[nameof(ApartmantReservation.ApartmentId)]),
                        Details = row[nameof(ApartmantReservation.Details)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(ApartmantReservation.CreatedAt)].ToString()),

                        UserId = (int)row[nameof(ApartmantReservation.UserId)],
                        User = LoadUser((int)row[nameof(ApartmantReservation.UserId)]),

                        UserName = row[nameof(ApartmantReservation.UserName)].ToString(),
                        UserEmail = row[nameof(ApartmantReservation.UserEmail)].ToString(),
                        UserPhone = row[nameof(ApartmantReservation.UserPhone)].ToString(),
                        UserAddress = row[nameof(ApartmantReservation.UserAddress)].ToString(),
                    });
            }

            return apartmantReservations;
        }

        public IList<ApartmantReservation> LoadApartmantReservationA(int apartmantId)
        {
            IList<ApartmantReservation> apartmantReservations = new List<ApartmantReservation>();

            var tblReservation = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmantReservationA), apartmantId).Tables[0];

            foreach (DataRow row in tblReservation.Rows)
            {
                apartmantReservations.Add(
                    new ApartmantReservation
                    {
                        Id = (int)row[nameof(ApartmantReservation.Id)],
                        Guid = (Guid)row[nameof(ApartmantReservation.Guid)],
                        ApartmentId = (int)row[nameof(ApartmantReservation.ApartmentId)],
                        Apartment = LoadApartment((int)row[nameof(ApartmantReservation.ApartmentId)]),
                        Details = row[nameof(ApartmantReservation.Details)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(ApartmantReservation.CreatedAt)].ToString()),

                        UserName = row[nameof(ApartmantReservation.UserName)].ToString(),
                        UserEmail = row[nameof(ApartmantReservation.UserEmail)].ToString(),
                        UserPhone = row[nameof(ApartmantReservation.UserPhone)].ToString(),
                        UserAddress = row[nameof(ApartmantReservation.UserAddress)].ToString(),
                    });
            }

            return apartmantReservations;
        }

        //-------------------------------------------------------------------------------- Data single --------------------------------------------------------------------------------

        public Tag LoadTag(int tagId)
        {
            var tblTag = SqlHelper.ExecuteDataset(CS, nameof(LoadTag), tagId).Tables[0];
            if (tblTag.Rows.Count == 0) return null;

            DataRow row = tblTag.Rows[0];
            return new Tag
            {
                Id = (int)row[nameof(Tag.Id)],
                Guid = (Guid)row[nameof(Tag.Guid)],
                Name = row[nameof(Tag.Name)].ToString(),
                NameEng = row[nameof(Tag.NameEng)].ToString(),
                TagType = LoadTagType((int)row[nameof(Tag.TypeId)]),
                TaggedApartmants = (int)row[nameof(Tag.TaggedApartmants)],
                CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString())
            };
        }

        public Apartment LoadApartment(int apartmentId)
        {
            var tblApartmant = SqlHelper.ExecuteDataset(CS, nameof(LoadApartment), apartmentId).Tables[0];
            if (tblApartmant.Rows.Count == 0) return null;

            DataRow row = tblApartmant.Rows[0];
            return new Apartment
            {
                Id = (int)row[nameof(Apartment.Id)],
                Guid = (Guid)row[nameof(Apartment.Guid)],
                Name = row[nameof(Apartment.Name)].ToString(),
                NameEng = row[nameof(Apartment.NameEng)].ToString(),
                Address = row[nameof(Apartment.Address)].ToString(),
                City = LoadCity((int)row[nameof(Apartment.CityId)]),
                CityName = row[nameof(Apartment.CityName)].ToString(),
                CityId = (int)row[nameof(Apartment.CityId)],
                Owner = LoadOwner((int)row[nameof(Apartment.OwnerId)]),
                OwnerName = row[nameof(Apartment.OwnerName)].ToString(),
                OwnerId = (int)row[nameof(Apartment.OwnerId)],
                Status = LoadStatus((int)row[nameof(Apartment.StatusId)]),
                StatusName = row[nameof(Apartment.StatusNameEng)].ToString(),
                StatusNameEng = row[nameof(Apartment.StatusNameEng)].ToString(),
                StatusId = (int)row[nameof(Apartment.StatusId)],
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                NumOfPictures = (int)row[nameof(Apartment.NumOfPictures)],
                Stars = LoadRating((int)row[nameof(Apartment.Id)]),
                ImgUrl = row[nameof(Apartment.ImgUrl)].ToString(),
                Price = (decimal)row[nameof(Apartment.Price)],
                CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString())
            };
        }

        public Status LoadStatus(int statusId)
        {
            var tblStatus = SqlHelper.ExecuteDataset(CS, nameof(LoadStatus), statusId).Tables[0];
            if (tblStatus.Rows.Count == 0) return null;

            DataRow row = tblStatus.Rows[0];
            return new Status
            {
                Id = (int)row[nameof(Status.Id)],
                Guid = (Guid)row[nameof(Status.Guid)],
                Name = row[nameof(Status.Name)].ToString(),
                NameEng = row[nameof(Status.NameEng)].ToString()
            };
        }

        public City LoadCity(int cityId)
        {
            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(LoadCity), cityId).Tables[0];
            if (tblCity.Rows.Count == 0) return null;

            DataRow row = tblCity.Rows[0];
            return new City
            {
                Id = (int)row[nameof(City.Id)],
                Guid = (Guid)row[nameof(City.Guid)],
                Name = row[nameof(City.Name)].ToString()
            };
        }

        public TagType LoadTagType(int tagTypeId)
        {
            var tblTagType = SqlHelper.ExecuteDataset(CS, nameof(LoadTagType), tagTypeId).Tables[0];
            if (tblTagType.Rows.Count == 0) return null;

            DataRow row = tblTagType.Rows[0];
            return new TagType
            {
                Id = (int)row[nameof(TagType.Id)],
                Guid = (Guid)row[nameof(TagType.Guid)],
                Name = row[nameof(TagType.Name)].ToString(),
                NameEng = row[nameof(TagType.NameEng)].ToString()
            };
        }


        public Owner LoadOwner(int ownerId)
        {
            var tblOwner = SqlHelper.ExecuteDataset(CS, nameof(LoadOwner), ownerId).Tables[0];
            if (tblOwner.Rows.Count == 0) return null;

            DataRow row = tblOwner.Rows[0];
            return new Owner
            {
                Id = (int)row[nameof(Owner.Id)],
                Guid = (Guid)row[nameof(Owner.Guid)],
                Name = row[nameof(Owner.Name)].ToString(),
                CreatedAt = DateTime.Parse(row[nameof(Owner.CreatedAt)].ToString())
            };
        }

        public User LoadUser(int userId)
        {
            var tblUser = SqlHelper.ExecuteDataset(CS, nameof(LoadUser), userId).Tables[0];
            if (tblUser.Rows.Count == 0) return null;

            DataRow row = tblUser.Rows[0];
            return new User
            {
                Id = row[nameof(User.Id)].ToString(),
                Guid = (Guid)row[nameof(User.Guid)],
                UserName = row[nameof(User.UserName)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                Password = row[nameof(User.Password)].ToString(),
                CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString())
            };
        }

        public ApartmantPicture LoadPicture(int id)
        {
            var tblPictures = SqlHelper.ExecuteDataset(CS, nameof(LoadPicture), id).Tables[0];
            if (tblPictures.Rows.Count == 0) return null;

            DataRow row = tblPictures.Rows[0];
            return new ApartmantPicture
            {
                Id = (int)row[nameof(ApartmantPicture.Id)],
                ApartmentId = (int)row[nameof(ApartmantPicture.ApartmentId)],
                Guid = (Guid)row[nameof(ApartmantPicture.Guid)],
                CreatedAt = DateTime.Parse(row[nameof(ApartmantPicture.CreatedAt)].ToString()),
                Bytes = row[nameof(ApartmantPicture.Bytes)].ToString(),
                Name = row[nameof(ApartmantPicture.Name)].ToString(),
                IsRepresentative = (bool)row[nameof(ApartmantPicture.IsRepresentative)]
            };
        }

    //-------------------------------------------------------------------------------- Data verification ------------------------------------------------------------------------------

    public bool DeletableTag(int tagId)
    {
        var tblDeletable = SqlHelper.ExecuteDataset(CS, nameof(DeletableTag), tagId).Tables[0];
        DataRow row = tblDeletable.Rows[0];
        if ((int)row[0] == 0) return true;
        else return false;
    }

    //-------------------------------------------------------------------------------- Data insert --------------------------------------------------------------------------------

    public void AddApartmant(Apartment apartment)
    {
        SqlHelper.ExecuteNonQuery(
            CS,
            nameof(AddApartmant),
            apartment.Name,
            apartment.NameEng,
            apartment.MaxAdults,
            apartment.MaxChildren,
            apartment.TotalRooms,
            apartment.Address,
            apartment.City.Id,
            apartment.Price,
            apartment.BeachDistance,
            apartment.Owner.Id,
            Guid.NewGuid());
    }

    public void AddTag(Tag tag)
    {
        SqlHelper.ExecuteNonQuery(
            CS,
            nameof(AddTag),
            tag.Name,
            tag.NameEng,
            tag.TagType.Id,
            Guid.NewGuid());
    }

    public void AddUser(User user)
    {
        SqlHelper.ExecuteNonQuery(
            CS,
            nameof(AddUser),
            user.UserName,
            user.Address,
            user.Email,
            user.PhoneNumber,
            user.Password,
            Guid.NewGuid());
    }
    public void AddPicture(ApartmantPicture picture)
    {
        SqlHelper.ExecuteNonQuery(
            CS,
            nameof(AddPicture),
            picture.Name,
            picture.Bytes,
            picture.ApartmentId,
            Guid.NewGuid());
    }
    public void AddTaggedApartmant(TaggedApartmant taggedApartmant)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(AddTaggedApartmant),
            taggedApartmant.TagId,
            taggedApartmant.ApartmantId,
            Guid.NewGuid());
    }

    public void AddReservationExistingUser(ApartmantReservation apartmantReservation)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(AddReservationExistingUser),
            apartmantReservation.Details,
            apartmantReservation.ApartmentId,
            apartmantReservation.UserId,
            Guid.NewGuid());
    }

    public void AddReservationAnonimusUser(ApartmantReservation apartmantReservation)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(AddReservationAnonimusUser),
            apartmantReservation.Details,
            apartmantReservation.ApartmentId,
            apartmantReservation.UserName,
            apartmantReservation.UserEmail,
            apartmantReservation.UserPhone,
            apartmantReservation.UserAddress,
            Guid.NewGuid());
    }

    public void AddReview(ApartmantReview review)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(AddReview),
            review.ApartmentId,
            review.UserId,
            review.Details,
            review.Stars,
            Guid.NewGuid());
    }

    //-------------------------------------------------------------------------------- Data update --------------------------------------------------------------------------------

    public void UpdateApartmant(Apartment apartment)
    {
        SqlHelper.ExecuteNonQuery(
            CS, nameof(UpdateApartmant),
            apartment.Id,
            apartment.Name,
            apartment.NameEng,
            apartment.MaxAdults,
            apartment.MaxChildren,
            apartment.TotalRooms,
            apartment.Address,
            apartment.City.Id,
            apartment.Price,
            apartment.Owner.Id,
            apartment.BeachDistance
        );
    }

    public void UpdateImage(ApartmantPicture apartmantPicture)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(UpdateImage),
            apartmantPicture.Id,
            apartmantPicture.Name,
            apartmantPicture.IsRepresentative
        );
    }

    public void SetRepresentative(int pictureId, int apartmantId)
    {
        SqlHelper.ExecuteNonQuery(CS, nameof(SetRepresentative), pictureId, apartmantId);
    }
    //-------------------------------------------------------------------------------- Data delete --------------------------------------------------------------------------------

    public void DeleteTag(int tagId)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(DeleteTag),
            tagId);
    }

    public void DeleteApartmant(int apartmantId)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(DeleteApartmant),
            apartmantId);
    }

    public void DeletePicture(int pictureId)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(DeletePicture),
            pictureId);
    }

    public void DeleteTaggedApartmant(int taggedApartmantId)
    {
        SqlHelper.ExecuteNonQuery(CS,
            nameof(DeleteTaggedApartmant),
            taggedApartmantId);
    }
}
}
