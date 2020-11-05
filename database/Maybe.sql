select transfer_id,(select username from users join accounts on accounts.user_id = users.user_id join transfers on account_from = account_id where transfers.account_from = @accountFrom),(select username from users join accounts on accounts.user_id = users.user_id join transfers on account_to = account_id where transfers.account_to = @accountTo), transfer_type_desc,transfer_status_desc, amount from transfers JOIN transfer_types t on t.transfer_type_id = transfers.transfer_type_id JOIN transfer_statuses s on s.transfer_status_id = transfers.transfer_status_id where transfer_id = @transfer_id

select username from users 
join accounts on accounts.user_id = users.user_id
join transfers on account_from = account_id
where transfers.account_from = accounts.account_id

select username from users 
join accounts on accounts.user_id = users.user_id
join transfers on account_to = account_id
where transfers.account_to = accounts.account_id