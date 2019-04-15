using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Parse;
using System.Threading.Tasks;

namespace Collect
{
    class ParseHandler
    {
        static ParseHandler toDoServiceInstance = new ParseHandler();
        public static ParseHandler Default { get { return toDoServiceInstance; } }
        public List<Collection> CollectionList;
        public List<CollectionItems> ItemList;
        public List<CollectionItems> DeleteItemsList;
        public string collectionID;


        private ParseHandler() { }

        public async Task<Boolean> Login (string username,string password)
        {
            try
            {                
                await ParseUser.LogInAsync(username, password);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Login Failed: " + e.Message);
                return false;
            }
        }
        public void Logout()
        {
            ParseUser.LogOut();
            var currentUser = ParseUser.CurrentUser; //This is null
        }
        public async void DeleteCollection(string ObjectName)
        {
            //--------------------------------
            //Deletes Collection
            //--------------------------------
            var Collection = ParseObject.GetQuery("Collection");
            ParseObject ObjToDelete = await Collection.GetAsync(SingletonB._instance.SelectedCollection);
            await ObjToDelete.DeleteAsync();

            //--------------------------------
            //Deletes Items
            //--------------------------------
            var Items = ParseObject.GetQuery("Item").WhereEqualTo("collection", ParseObject.CreateWithoutData("Collection", SingletonB._instance.SelectedCollection));
            var ItemsToDelete = await Items.FindAsync();

            DeleteItemsList = new List<CollectionItems>();
            foreach (var obj in ItemsToDelete)
            {
                await obj.DeleteAsync();
            }
        }
        public async void DeleteItem(string ObjectID)
        {
            var Item = ParseObject.GetQuery("Item");
            ParseObject ItemToDelete = await Item.GetAsync(SingletonB._instance.ItemID);
            await ItemToDelete.DeleteAsync();
        }
        public async Task<Boolean> CheckIfUsernameExists (string username)
        {
            var query = ParseUser.Query;
            var queryresult = await query.WhereEqualTo("username", username).FindAsync();

            if (queryresult.ToList ().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CollectionSelected(string SelectedCollectionID)
        {
            collectionID = SelectedCollectionID;
        }
        

        public async Task<List<Collection>> GetAllCollections()
        {
            var query = ParseObject.GetQuery("Collection").WhereEqualTo("owner",ParseUser.CurrentUser);
            var result = await query.FindAsync();

            CollectionList = new List<Collection>();

            foreach (var obj in result)
            {

                Collection tempobj = new Collection();

                tempobj.ObjectId = obj.ObjectId;
                tempobj.CreatedAt = obj.CreatedAt;
                tempobj.UpdatedAt = obj.UpdatedAt;

                ParseUser usrobj = obj.Get<ParseUser>("owner");
                tempobj.Owner = await usrobj.FetchIfNeededAsync();

                //tempobj.Image = obj.Get<ParseFile>("image");
                tempobj.Name = Convert.ToString(obj["name"]);

                CollectionList.Add(tempobj);
            }

            return CollectionList;

        }
        public async Task<List<CollectionItems>> GetItems()
        {
            var query = ParseObject.GetQuery("Item").WhereEqualTo("collection", ParseObject.CreateWithoutData("Collection",collectionID));
            var result = await query.FindAsync();

            ItemList = new List<CollectionItems>();

            foreach (var obj in result)
            {

                CollectionItems tempobj = new CollectionItems();

                tempobj.ObjectId = obj.ObjectId;
                tempobj.CreatedAt = obj.CreatedAt;
                tempobj.UpdatedAt = obj.UpdatedAt;

                ParseUser usrobj = obj.Get<ParseUser>("owner");
                tempobj.Owner = await usrobj.FetchIfNeededAsync();

                tempobj.Name = Convert.ToString(obj["name"]);

                if (obj["description"] != null)
                {
                    tempobj.Description = Convert.ToString(obj["description"]);
                }
                else
                {
                    tempobj.Description = "";
                }
                if (obj["website"] != null)
                {
                    tempobj.Website = Convert.ToString(obj["website"]);
                }
                else
                {
                    tempobj.Website = "";
                }
                
                
                ItemList.Add(tempobj);
            }

            return ItemList;

        }

        public async Task UpdateItem(string Title, string Description, string website)
        {
            var query = ParseObject.GetQuery("Item").WhereEqualTo("objectId", SingletonB._instance.ItemID);



            ParseObject Update = await query.GetAsync(SingletonB._instance.ItemID);            

            Update["name"] = Title;
            Update["description"] = Description;
            Update["website"] = website;



            await Update.SaveAsync();
        }
        public async Task SaveItem(string title, string description, string website)
        {

            var query = ParseObject.GetQuery("Collection").WhereEqualTo("objectId", SingletonB._instance.SelectedCollection);



            ParseObject CollectionObj = await query.GetAsync(SingletonB._instance.SelectedCollection);

            ParseObject Item = new ParseObject("Item");
            Item["name"] = title;
            Item["description"] = description;
            Item["website"] = website;
            Item["collection"] = CollectionObj;
            Item["owner"] = ParseUser.CurrentUser;
            await Item.SaveAsync();
        }
        
        public async Task CreateCollection(string title)
        {
            ParseObject Collection = new ParseObject("Collection");
            Collection["name"] = title;
            Collection["owner"] = ParseUser.CurrentUser;
            await Collection.SaveAsync();
        }

        public async Task CreateUserAsync (string email, string username, string password)
        {
            try
            {
                var user = new ParseUser()
                {
                    Username = username,
                    Password = password,
                    Email = email
                };
                await user.SignUpAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Registration Failed: " + e.Message);
            }
            
        }

        public async Task GetCollectionIDs (string username)
        {
            var query = from Collection in ParseObject.GetQuery("Collection")
                        where Collection.Get<string>("username") == username
                        select Collection;
            IEnumerable<ParseObject> results = await query.FindAsync();
            Console.WriteLine(results.ToString());
            
        }


    }
}