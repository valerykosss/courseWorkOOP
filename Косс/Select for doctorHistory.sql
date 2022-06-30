SELECT * FROM REGISTRATION_DATE
INNER JOIN REGISTRATION_TIME
ON REGISTRATION_DATE.TimeID = REGISTRATION_TIME.TimeID
INNER JOIN REGISTRATION
ON REGISTRATION.DateID = REGISTRATION_DATE.DateID
INNER JOIN DOCTOR_SERVICE
ON REGISTRATION.DoctorServiceID = DOCTOR_SERVICE.DoctorServiceID
INNER JOIN DOCTOR 
ON DOCTOR_SERVICE.DoctorID = DOCTOR.DoctorID
INNER JOIN SERVICE 
ON DOCTOR_SERVICE.ServiceID = SERVICE.ServiceID
INNER JOIN CLIENT 
ON REGISTRATION.ClientID = CLIENT.ClientID
WHERE REGISTRATION.DoctorServiceID=DOCTOR_SERVICE.DoctorServiceID
and DOCTOR.DoctorSurname='Одинцова'




--from r_time in db.REGISTRATION_TIME
--                                join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
--                                join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
--                                join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
--                                where DateTime.Compare(r_date.Date, selectedDate) == 0 && doc_serv.DoctorID.ToString() == DoctorsButtonUserControl.Id 
--                                && doc_serv.ServiceID.ToString() == ServiceButtonUserControl.idOfChosenService