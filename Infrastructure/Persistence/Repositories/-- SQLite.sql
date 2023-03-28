-- SQLite
-- GetAll Ronots And Weapons
select * from Robot as r
left join RobotWeapon as rw
on r.Id = rw.RobotId
left join Weapon as w
on w.Id = rw.WeaponId

-- Get One Robot And Weapons
SELECT * FROM Robot WHERE Id = 4;
SELECT * 
FROM Weapon AS w
INNER JOIN RobotWeapon AS rw
ON rw.RobotId = 4 AND rw.WeaponId = w.Id;