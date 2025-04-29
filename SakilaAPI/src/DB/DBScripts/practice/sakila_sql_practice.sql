SELECT * FROM sakila.actor;

SELECT
	a.last_name AS Lastname
FROM
	Actor a
Where actor_id = 17;

SELECT a.actor_id,
		a.first_name,
        a.last_name,
        f.title,
        l.name AS `language`        
FROM Actor a
JOIN film_actor AS fa ON a.actor_id = fa.actor_id
JOIN Film AS f ON fa.film_id = f.film_id
JOIN `language` AS l ON f.language_id = l.language_id
WHERE l.name = 'english';
        
        SELECT
  a.actor_id,
  a.first_name,
  a.last_name,
  COUNT(*)               AS total_english_films
FROM actor AS a
JOIN film_actor AS fa
  ON a.actor_id = fa.actor_id
JOIN film AS f
  ON fa.film_id = f.film_id
JOIN `language` AS l
  ON f.language_id = l.language_id
WHERE l.name = 'English'
GROUP BY
  a.actor_id,
  a.first_name,
  a.last_name
ORDER BY
  total_english_films DESC;


        