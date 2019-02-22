delete from reservation;
delete from site;
delete from campground;
delete from park;

insert into park values ('garage', 'ohio','1753-01-01',1,2);
declare @park1 int = @@identity;
insert into park values ('car park','england','1776-02-03',50,5);
declare @park2 int = @@identity;

insert into campground values (@park1,'taurus',2,4,25.00);
declare @campground1 int = @@identity;
insert into campground values (@park1, 'stratus',4,6,30.00);
declare @campgroud2 int = @@identity;
insert into campground values (@park2,'neon',8,12,15.00);
declare @campground3 int = @@identity;

insert into site values (@campground1, 1,5,0,15,0);
declare @site1 int = @@identity;
insert into site values (@campground1, 2,6,1,0,1);
declare @site2 int = @@identity;
insert into site values (@campground2, 1);
declare @site3 int = @@identity;
insert into site values (@campground2, 2,1,1,12,1);
declare @site4 int = @@identity;
insert into site values (@campground3, 1);
declare @site5 int = @@identity;

insert into reservation values (@site1, 'gary trello', '2019-02-01','2019-02-28');
insert into reservation values (@site2, 'jason vorhees','2019-03-01','2019-03-29');
insert into reservation values (@site3, 'river ryver','2019-02-15','2019-03-15');