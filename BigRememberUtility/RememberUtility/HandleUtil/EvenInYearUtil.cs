using System;
using System.Collections.Generic;
using System.Linq;
using RememberUtility.Constant;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.Interface;
using RememberUtility.Model;

namespace RememberUtility.HandleUtil
{
    public class EvenInYearUtil : IEventInYear

    {
        private readonly FileHandlerUtil _fileHandlerUtil;

        public EvenInYearUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
            _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.EVENTINYEAR);
        }

        public void AddEvent(EventInYear eventInYear)
        {
            eventInYear.CreatedDate = $"{DateTime.Now:MMMM dd, yyyy}";
            _fileHandlerUtil.JsonModel.EventInYears.Add(eventInYear);

            _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);
        }

        public EventInYear FindEventInYear(string eventName)
        {
            var getEvent = _fileHandlerUtil.JsonModel.EventInYears
                .Find(x => string.Equals(x.EventName, eventName, StringComparison.CurrentCultureIgnoreCase));

            if (getEvent == null) return null;
            _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);

            return getEvent;
        }

        public bool UpdateEvent(string currentEventName, string newEventName, string newCountryOccured, string newEventLink,
            string newEventDate, string newShortDescribe)
        {
            var getCurrentEvent = _fileHandlerUtil.JsonModel.EventInYears.
                Find(x => string.Equals(x.EventName, currentEventName, StringComparison.CurrentCultureIgnoreCase));
            var indexOfEvent = _fileHandlerUtil.JsonModel.EventInYears.IndexOf(getCurrentEvent);

            if (getCurrentEvent != null)
            {
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].EventName = newEventName;
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].CountryOccured = newCountryOccured;
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].EventLink = newEventLink;
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].EventDate = newEventDate;
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].ShortDescribe = newShortDescribe;
                _fileHandlerUtil.JsonModel.EventInYears[indexOfEvent].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";

                _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);
            return false;
        }

        public bool DeleteEvent(string eventName)
        {
            if (eventName == null) return false;

            var getCurrentEvent = _fileHandlerUtil.JsonModel.EventInYears.Find(x =>
                string.Equals(x.EventName, eventName, StringComparison.CurrentCultureIgnoreCase));

            if (getCurrentEvent == null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);

                return false;
            }

            _fileHandlerUtil.JsonModel.EventInYears.Remove(getCurrentEvent);

            _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);

            return true;
        }

        public List<EventInYear> GetEventInYears()
        {
            var list = _fileHandlerUtil.JsonModel.EventInYears.ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.EVENTINYEAR);

            return list;
        }
    }
}
