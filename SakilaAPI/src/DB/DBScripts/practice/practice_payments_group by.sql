SELECT * FROM sakila.payment;

SELECT
 payment.customer_id, 
 customer.email,
 DATE(payment_date),
 COUNT(payment_id) AS 'Number of Payments'
FROM payment
JOIN customer ON customer.customer_id = payment.customer_id
GROUP BY customer_id, DATE(payment_date);

SELECT
 customer_id AS CustomerID,
 COUNT(payment_id) AS Payments
 FROM payment
 WHERE customer_id = 1;
 
 SELECT
 customer_id AS CustomerID,
 COUNT(payment_id) AS Payments
 FROM payment
 GROUP BY customer_id;
 
 SELECT 
 payment_id,
 customer_id, 
 amount
 FROM payment
 WHERE amount < 1;
 
SELECT 
 p.customer_id, 
 SUM(p.amount)
 FROM payment AS p
 GROUP BY customer_id
 ORDER BY SUM(p.AMOUNT) DESC;

SELECT 
 p.customer_id, 
 s.store_id,
 SUM(p.amount) AS total_amount
 FROM payment AS p
 JOIN customer c ON c.customer_id = p.customer_id
 JOIN store AS s on s.store_id = c.store_id
 GROUP BY customer_id
 ORDER BY SUM(p.AMOUNT) DESC;
 
 
SELECT 
 s.store_id,
 SUM(p.amount) AS total_amount
 FROM payment AS p
 JOIN customer c ON c.customer_id = p.customer_id
 JOIN store AS s on s.store_id = c.store_id
 GROUP BY s.store_id
 ORDER BY SUM(p.AMOUNT) DESC;
 
 
 
 
 
 