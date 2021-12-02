using AIIVE.BookReview.Core.DomainObjects;

namespace AIIVE.BookReview.Catalogo.Domain
{
    public class Book : Entity<long>, IAggregateRoot
    {
        public string Authors { get;  set; }

        public string ISBN { get; set; }

        public int? OriginalPublicationYear { get; set; }

        public string OriginalTitle { get; set; }
        public string Title { get;  set; }

        public string LanguageCode { get; set; }

        public float AverageRating { get;  set; }

        public string ImageUrl { get;  set; }

        public string SmallImageUrl { get;  set; }

        public Book()
        {

        }

        public Book(
            string authors,
            string isbn,            
            string originalTitle,
            string title,
            string languageCode,
            float averageRating,
            string imageUrl,
            string smallImageUrl,
            int? originalPublicationYear = null)
        {
            Authors = authors;
            ISBN = isbn;
            OriginalPublicationYear = originalPublicationYear;
            OriginalTitle = originalTitle;
            Title = title;
            LanguageCode = languageCode;
            AverageRating = averageRating;
            ImageUrl = imageUrl;
            SmallImageUrl = smallImageUrl;
        }
        public Book(
            long id,
            string authors,
            string isbn,            
            string originalTitle,
            string title,
            string languageCode,
            float averageRating,
            string imageUrl,
            string smallImageUrl,
            int? originalPublicationYear = null) : this(
            authors,
            isbn,            
            originalTitle,
            title,
            languageCode,
            averageRating,
            imageUrl,
            smallImageUrl,
            originalPublicationYear)
        {
            Id = id;            
        }
        
    }
}
