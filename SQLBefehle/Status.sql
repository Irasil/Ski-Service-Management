use Ski_Service;

SET IDENTITY_INSERT Status ON


insert into Status ( StatusId, StatusName)
values
(
	1,
	'Offen'
);

insert into Status ( StatusId, StatusName)
values
(
	2,
	'In Arbeit'
);

insert into Status ( StatusId, StatusName)
values
(
	3,
	'Abgeschlossen'
);


SET IDENTITY_INSERT Status OFF