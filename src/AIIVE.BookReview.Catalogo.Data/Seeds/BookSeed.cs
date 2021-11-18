using AIIVE.BookReview.Catalogo.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace AIIVE.BookReview.Catalogo.Data.Seeds
{
    public class BookSeed
    {
        
        
        /*
         * 0 book_id,
         * 1 goodreads_book_id,
         * 2 best_book_id,
         * 3 work_id,
         * 4 books_count,
         * 5 isbn,
         * 6 isbn13,
         * 7 authors,
         * 8 original_publication_year,
         * 9 original_title,
         * 10 title,
         * 11 language_code,
         * 12 average_rating,
         * 13 ratings_count,
         * 14 work_ratings_count,
         * 15 work_text_reviews_count,
         * 16 ratings_1,
         * 17 ratings_2,
         * 18 ratings_3,
         * 19 ratings_4,
         * 20 ratings_5,
         * 21 image_url,
         * 22 small_image_url         
         */

        public static IEnumerable<object> Create()
        {
            using var reader = new StreamReader("books.csv");

            Console.WriteLine(reader.ReadLine());
            

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var properties = line.Split(";");

                Console.WriteLine(properties[8]);

                int.TryParse(properties[8], out int year);

                yield return new Book(
                    id: long.Parse(properties[0]),
                    authors: properties[7],
                    isbn: properties[5],
                    originalPublicationYear: year,
                    originalTitle: properties[9],
                    title: properties[10],
                    languageCode: properties[11],
                    averageRating: float.Parse(properties[12]),
                    imageUrl: properties[21],
                    smallImageUrl: properties[22]);
            }
        }
    }
}
