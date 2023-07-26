select * from state_table;
select * from country_table;
insert into state_table values (4,2,'Dhaka');

ALTER TABLE state_table
ADD FOREIGN KEY (countryId) REFERENCES country_table(countryId);

select * from country_table c, state_table s where c.countryId = s.countryId;
