
using System;
using System.Text;
using System.Threading;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;


namespace EventInYearSampleCode
{
    public class Program
    {
        private static void Main()
        {
            var eventUtil = new EvenInYearUtil();
            int choose;
            Console.OutputEncoding = Encoding.UTF8;
            do
            {
                Console.WriteLine("1. Get list events");
                Console.WriteLine("2. Add event");
                Console.WriteLine("3. Find event");
                Console.WriteLine("4. Update event");
                Console.WriteLine("5. Delete event");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1: // add et
                        foreach (var itemEvent in eventUtil.GetEventInYears())
                        {
                            Console.WriteLine("Event Name: " + itemEvent.EventName);
                            Console.WriteLine("Links: " + itemEvent.EventLink);
                            Console.WriteLine("Country Occured: " + itemEvent.CountryOccured);
                            Console.WriteLine("Event Date: " + itemEvent.EventDate);
                            Console.WriteLine("Describe: " + itemEvent.ShortDescribe);
                            Console.WriteLine("------------------------------");
                        }
                        break;

                    case 2: // add event
                        Console.Write("Event name: ");
                        var eventName = Console.ReadLine();

                        Console.Write("Event Link: ");
                        var link = Console.ReadLine();

                        Console.Write("Event Date: ");
                        var eventDate = Console.ReadLine();

                        Console.Write("Describe: ");
                        var shortDescribe = Console.ReadLine();

                        Console.WriteLine("Types of Category");
                        string countryOccured;
                        var count = 1;
                        var listConstantValue = typeof(CategoriesCountryOccuredConstant).GetAllPublicConstantValues<string>();
                        listConstantValue.Sort();
                        foreach (var propertyInfo in listConstantValue)
                        {
                            Console.WriteLine(count++ + ". " + propertyInfo);
                        }

                        Console.WriteLine("Please choose a Country: ");
                        countryOccured = Console.ReadLine();

                        if (countryOccured != null)
                        {
                            if (int.Parse(countryOccured) > 0 && int.Parse(countryOccured) <= listConstantValue.Count)
                            {
                                countryOccured = listConstantValue[int.Parse(countryOccured) - 1];
                            }
                        }

                        eventUtil.AddEvent(new EventInYear()
                        {
                            EventName = eventName, EventDate = eventDate, 
                            EventLink = link,
                            CountryOccured = countryOccured,
                            ShortDescribe = shortDescribe,
                            EventYearId = HandleRandom.RandomString(8)
                        });
                        HandleRandom.ChooseColorForString("Adding succesful", ConsoleColor.Blue);

                        break;

                    case 3: // find et
                        Console.Write("Et Name: ");
                        var findEtName = Console.ReadLine();
                        var result = eventUtil.FindEventInYear(findEtName);
                        if (result != null)
                        {
                            Console.WriteLine("Event Name: " + result.EventName);
                            Console.WriteLine("Links: " + result.EventLink);
                            Console.WriteLine("Country Occured: " + result.CountryOccured);
                            Console.WriteLine("Event Date: " + result.EventDate);
                            Console.WriteLine("Describe: " + result.ShortDescribe);
                            Console.WriteLine("------------------------------");
                            
                            break;
                        }

                        HandleRandom.ChooseColorForString("There are no Et you find", ConsoleColor.Blue);
                        break;

                    case 4: // update Et
                        Console.Write("Event Name to update: ");
                        var placeHoldEtName = Console.ReadLine();

                        if (eventUtil.FindEventInYear(placeHoldEtName) != null)
                        {
                            var currentEtResult = eventUtil.FindEventInYear(placeHoldEtName);
                            HandleRandom.ChooseColorForString("Found Event", ConsoleColor.Blue);
                            Console.WriteLine("Event info---");
                            Console.WriteLine("Id: " + currentEtResult.EventYearId);
                            Console.WriteLine("Et Name: " + currentEtResult.EventName);
                            Console.WriteLine("Link: " + currentEtResult.EventLink);
                            Console.WriteLine("Country Occured: " + currentEtResult.CountryOccured);
                            Console.WriteLine("Describe: " + currentEtResult.ShortDescribe);
                            Console.WriteLine("---------------------------");

                            Console.Write("Event name to update: ");
                            var eventNameToUpdate = Console.ReadLine();

                            
                            Console.Write("Event Link to update: ");
                            var eventLinkToUpdate = Console.ReadLine();

                            Console.Write("Event Date to update: ");
                            var eventDateToUpdate = Console.ReadLine();

                            Console.WriteLine("Describe to update: ");
                            var newShortDescribe = Console.ReadLine();

                            Console.WriteLine("Countries");
                            var counts = 1;
                            var listConstantValues = typeof(CategoriesCountryOccuredConstant).GetAllPublicConstantValues<string>();
                            listConstantValues.Sort();
                            foreach (var propertyInfo in listConstantValues)
                            {
                                Console.WriteLine(counts++ + ". " + propertyInfo);
                            }

                            Console.Write("Please choose a country to update: ");
                            var countryToUpdate = Console.ReadLine();

                            if (countryToUpdate != null)
                            {
                                if (int.Parse(countryToUpdate) > 0 && int.Parse(countryToUpdate) <= listConstantValues.Count)
                                {
                                    countryToUpdate = listConstantValues[int.Parse(countryToUpdate) - 1];
                                }
                            }

                            if (eventUtil.UpdateEvent(currentEtResult.EventName, eventNameToUpdate, countryToUpdate,
                                eventLinkToUpdate, eventDateToUpdate, newShortDescribe))
                            {
                                HandleRandom.ChooseColorForString("Update event success", ConsoleColor.Blue);
                                break;
                            }
                            HandleRandom.ChooseColorForString("Update event failed", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            HandleRandom.ChooseColorForString("Nothing found event", ConsoleColor.DarkRed);
                        }

                        break;
                    case 5: // delete et
                        Console.Write("Et name to delete:");
                        var findEventToDelete = Console.ReadLine();
                        if (eventUtil.DeleteEvent(findEventToDelete))
                        {
                            HandleRandom.ChooseColorForString("Deleted successful", ConsoleColor.Blue);
                            break;
                        }
                        HandleRandom.ChooseColorForString("Nothing event name to delete", ConsoleColor.DarkRed);
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString("There is no option you chose", ConsoleColor.Blue);
            Thread.Sleep(2000);
        }
    }
}
