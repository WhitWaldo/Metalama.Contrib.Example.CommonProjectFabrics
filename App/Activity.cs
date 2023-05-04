using Models;

namespace App;

public class Activity
{
    private readonly Repository<Person> _people;

    public Activity(Repository<Person> peopleRepo)
    {
        _people = peopleRepo;
    }

    public void DoOperations()
    {
        //Create
        //_people.Add(new Person("Andy"));
        //_people.Add(new Person("Bill"));
        //_people.Add(new Person("Charlie"));

        //List
        //var people = _people.List();

        ////Get
        //var bill = _people.Get(people.First(person => person.Name == "Bill").Id);
        //if (bill is not null)
        //{
        //    var updatedBill = new Person("Billy")
        //    {
        //        Id = bill.Id
        //    };

        //    //Update
        //    _people.Update(updatedBill);
        //}

        ////Delete
        //_people.Delete(people.First(person => person.Name == "Andy").Id);

        ////List #2
        //var remainingPeople = _people.List();
        //Console.WriteLine($"There are {remainingPeople.Count} remaining people in the repository");
    }
}