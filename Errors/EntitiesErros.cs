using Restaurants_Platform.Shared;

namespace Restaurants_Platform.Errors;


public class EntitiesErrors
{
    public class Review
    {
        public static readonly Error ContentAndRateIsNull= new Error(
            "Review.ContentAndRateIsNull",
            "Both content and star rating is be null.");

        public static Error NotFound(Guid Id) => new Error(
            "Review.NotFound",
            "Review with {Id} does not exit"
            );
    }
}
