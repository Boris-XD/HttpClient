

using OrderNested.Models;
using System.Linq.Expressions;
using OrderNested.Extensions;
using System;
using System.Runtime.CompilerServices;
using OrderNested.Enum;

List<Person> persons = new List<Person>()
{
    new Person()
    {
        Id = 5,
        Name = "Pedro",
        LastName = "Sarmiento",
        Address = new Address()
        {
            Id = 12,
            Direction = "Mangales",
            Number = 1,
        }
    },
    new Person()
    {
        Id = 8,
        Name = "Alex",
        LastName = "Gonzales",
        Address = new Address()
        {
            Id = 30,
            Direction = "Alejandro Poveda",
            Number = 1,
        }
    },
    new Person()
    {
        Id = 1,
        Name = "Zoila",
        LastName = "Sanchez",
        Address = new Address()
        {
            Id = 122,
            Direction = "Walter froid",
            Number = 1,
        }
    }
};


foreach (var person in persons)
{
    Console.WriteLine(person.Name);
}



var parameter = Expression.Parameter(typeof(Person), "");


var orderByKeySelectorNested = Expression.Lambda<Func<Person, string>>(
    Expression.Property(
        Expression.Property(parameter, "Address"),
        "Direction"),
    parameter);





var orderByKeySelectorSingle = Expression.Lambda<Func<Person, int>>(
    Expression.Property(parameter, "Id"),
    parameter);


Expression<Func<TEntity, TKey>> GetOrderByKeySelector<TEntity, TKey>(
    string nestedObjectPropertyName,
    string nestedObjectFieldToOrderByPropertyName)
{
    var parameter = Expression.Parameter(typeof(TEntity));
    var orderByKeySelector = Expression.Lambda<Func<TEntity, TKey>>(
        Expression.Property(
            Expression.Property(parameter, nestedObjectPropertyName),
            nestedObjectFieldToOrderByPropertyName),
        parameter);

    return orderByKeySelector;
}

var orders = GetOrderByKeySelector<Person, string>("Address", "Direction");



var result = persons.AsQueryable().OrderBy(orders, true);

Console.WriteLine("---------------------------------------");

foreach (var person in result)
{
    Console.WriteLine(person.Name);
}


object miObjeto = new Person();

Type tipoPropiedad = miObjeto.GetType().GetProperty("Name").PropertyType;


Console.WriteLine("---------------------------------------");

Console.WriteLine(tipoPropiedad.Name);
Console.WriteLine("---------------------------------------");

Console.WriteLine(tipoPropiedad.GetType());


Type tipoPropiedadAnidada = miObjeto.GetType().GetProperty("Address").PropertyType.GetProperty("Direction").PropertyType;

Console.WriteLine("---------------------------------------");

Console.WriteLine(tipoPropiedadAnidada);




/*
T GetPetFieldFromString<T>(string field)
{
    T fieldProperty;

    if (Enum.TryParse(field, out fieldProperty))
    {
        fieldProperty = (PersonEnum)Enum.Parse(typeof(PersonEnum), field);
    }
    else
    {
        fieldProperty = T.Name;
    }

    return fieldProperty;
}*/

string param = ".";
/*
bool ValidateEnum<T>(string field) where T : Enum
{
    T fieldProperty;

    if (Enum.TryParse(field, out fieldProperty))
        return true;
    else
        return false;
}*/

bool validateEnumPerson(string field)
{
    PersonEnum fieldProperty;

    if (Enum.TryParse(field, out fieldProperty))
        return true;
    else
        return false;
}

bool validateEnumAddress(string field)
{
    AddressEnum fieldProperty;

    if (Enum.TryParse(field, out fieldProperty))
        return true;
    else
        return false;
}

/*
bool 




bool isNested = param.Contains(".");
if (isNested)
{
    string[] propParam = param.Split('.');
    string nameObject = propParam[0];
    string propObject = propParam[1];

    Console.WriteLine(nameObject);
    Console.WriteLine(propObject);

    var firstProp = validateEnumPerson(nameObject);
    var secondProp = validateEnumPerson(propObject);

}
else
{

}

*/




