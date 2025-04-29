SELECT * FROM sakila.actor;

SELECT
  MAX(actor_id)      AS MaxActorInFilmActor,
  COUNT(*)           AS CountFor86
FROM
  sakila.film_actor
WHERE
  actor_id = 86;
  
-- Indexed lookup
SELECT SQL_NO_CACHE *
FROM actor
WHERE last_name = 'Pitt';

-- Non-indexed lookup
SELECT SQL_NO_CACHE *
FROM actor
WHERE first_name = 'James';

# Costs

#time---

EXPLAIN ANALYZE
SELECT *
FROM actor
WHERE last_name = 'Guiness';

EXPLAIN ANALYZE
SELECT *
FROM actor
WHERE first_name = 'jayne';


