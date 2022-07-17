using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Dal
{
    public interface IRepo
    {
        User AuthenticateUser(string username, string password);
        IList<City> LoadCities();
        City LoadCity(int cityId);
        IList<User> LoadUsers();
        User LoadUser(int userId);
        IList<Owner> LoadOwners();
        Owner LoadOwner(int ownerId);
        IList<Tag> LoadTags();
        Tag LoadTag(int tagId);
        IList<Apartment> LoadFilteredApartments(string rooms, string adults, string children, string cityId);
        IList<Apartment> LoadApartments();
        Apartment LoadApartment(int apartmentId);
        IList<Status> LoadStatuses();
        Status LoadStatus(int statusId);
        IList<TagType> LoadTagTypes();
        TagType LoadTagType(int tagId);
        IList<TaggedApartmant> LoadTaggedApartmant(int apartmantId);
        IList<ApartmantPicture> LoadPictures(int apartmantId);
        ApartmantPicture LoadPicture(int id);
        IList<ApartmantReservation> LoadApartmantReservationE(int apartmantId);
        IList<ApartmantReservation> LoadApartmantReservationA(int apartmantId);
        bool DeletableTag(int tagId);
        void AddUser(User item);
        void AddTag(Tag item);
        void AddApartmant(Apartment item);
        void AddReview(ApartmantReview review);
        void AddPicture(ApartmantPicture picture);
        void AddTaggedApartmant(TaggedApartmant taggedApartmant);
        void AddReservationExistingUser(ApartmantReservation apartmantReservation);
        void AddReservationAnonimusUser(ApartmantReservation apartmantReservation);
        void UpdateApartmant(Apartment apartment);
        void UpdateImage(ApartmantPicture apartmantPicture);
        void DeleteTag(int tagId);
        void DeleteTaggedApartmant(int taggedApartmantId);
        void SetRepresentative(int pictureId, int apartmantId);
        void DeleteApartmant(int tagId);
        void DeletePicture(int pictureId);
    }
}
