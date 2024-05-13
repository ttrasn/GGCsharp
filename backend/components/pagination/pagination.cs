using MongoDB.Driver;

namespace backend.components.pagination;

public class Pagination<T>
{
    public int Page { set; get; }
    public int PageSize { set; get; }
    public string Sort { set; get; }
    public long TotalItems { set; get; }
    public long TotalPages { set; get; }
    public List<T> Data { set; get; }
    public IMongoCollection<T> collection;

    public Pagination(IMongoCollection<T> collection, Request request)
    {
        this.Page = request.Page ?? 1;
        this.PageSize = request.PageSize ?? 20;
        this.Sort = request.Sort;
        this.collection = collection;
    }

    public Pagination<T> GetList(FilterDefinition<T> filter)
    {
        this.TotalItems = this.collection.Find(filter).Count();
        if (this.TotalItems == 0)
        {
            return this;
        }

        this.TotalPages = (int)Math.Ceiling(((double)this.TotalItems / (double)this.PageSize));
        var query = this.collection.Find(filter).Limit(this.PageSize).Skip((this.Page - 1) * this.PageSize);
        if (this.Sort != null && this.Sort.Length > 0)
        {
            if (this.Sort.StartsWith("-"))
            {
                var sort = Builders<T>.Sort.Descending(this.Sort.Substring(1));
                query = query.Sort(sort);
            }
            else
            {
                var sort = Builders<T>.Sort.Ascending(this.Sort);
                query = query.Sort(sort);
            }
        }

        this.Data = query.ToList();
        return this;
    }
}