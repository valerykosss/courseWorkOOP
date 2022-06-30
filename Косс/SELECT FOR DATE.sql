SELECT * FROM REGISTRATION_DATE
INNER JOIN REGISTRATION_TIME
ON REGISTRATION_DATE.TimeID = REGISTRATION_TIME.TimeID
INNER JOIN REGISTRATION
ON REGISTRATION.DateID = REGISTRATION_DATE.DateID
INNER JOIN DOCTOR_SERVICE
ON REGISTRATION.DoctorServiceID = DOCTOR_SERVICE.DoctorServiceID
WHERE REGISTRATION_DATE.Date = '2022-05-21' and DOCTOR_SERVICE.DoctorID = 2 and DOCTOR_SERVICE.ServiceID = 1



--from r_time in db.REGISTRATION_TIME
--                                join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
--                                join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
--                                join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
--                                where DateTime.Compare(r_date.Date, selectedDate) == 0 && doc_serv.DoctorID.ToString() == DoctorsButtonUserControl.Id 
--                                && doc_serv.ServiceID.ToString() == ServiceButtonUserControl.idOfChosenService