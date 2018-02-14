using SWC.Data.Entity;
using System.Collections.Generic;

namespace SWC.Data.Interface
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// Fetch tasks by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<clsTask> LoadTasksByPlan(int id);

        /// <summary>
        /// Update description of the scheduleitemid/userid
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="scheduleitemid"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        bool UpdateScheduleStaff(int userid, int scheduleitemid, string description);

        /// <summary>
        /// Insert ScheduleItem
        /// </summary>
        /// <param name="scheduleitemid"></param>
        /// <param name="staff_name"></param>
        /// <param name="description"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        bool InsertSchedulePaymentStaff(int scheduleitemid, string staff_name, string description, float total);

        /// <summary>
        /// Fetch ScheduleItem by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        clsScheduleItem GetScheduleItem(int id);

        /// <summary>
        /// Fetch Images of the ScheduleItem
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        IList<clsImage> LoadImagesByItemId(int itemid);

        /// <summary>
        /// Fetch tasks by ScheduleItemId 
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        IList<clsTask> LoadTasksByItemId(int itemid);

        /// <summary>
        /// Fetch schedule by userid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        IList<clsSchedule> LoadScheduleByUser(int userid);

        /// <summary>
        /// Fetch extra tasks
        /// </summary>
        /// <returns></returns>
        List<clsTask> LoadExtraTasks();

        /// <summary>
        /// Fetch plans
        /// </summary>
        /// <returns></returns>
        List<clsPlan> LoadPlan();

        /// <summary>
        /// Save Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <param name="periodtypeid"></param>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        bool SaveScheduleItem(int id, int userid, int periodtypeid, string time, string date);
    }
}
