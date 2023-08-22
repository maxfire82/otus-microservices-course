from locust import HttpUser, User, task, between

class FlowException(Exception):
   pass

class UserBehavior(HttpUser):
    wait_time = between(1, 5)

    @task   
    def user_flow(self):
        # create user
        data = {
            "firstName": "Иван",
            "lastName": "Иванов",
            "email": "test@mail.ru",
            "phone": "8909001212",
            "userName": "user1"
        }
        headers = {'Host':'arch.homework'}
        response = self.client.post("/user", json = data, 
            headers=headers, 
            name = "Create a new user")

        if response.status_code != 200:
           raise FlowException('user not created')

        user_id = response.json().get('id')

        # get user 
        self.client.get(f"/user/{user_id}", 
            headers=headers, 
            name = "Get user")

        # update user 
        data = {
            "firstName": "Геннадий",
            "lastName": "Иванов",
            "email": "test@mail.ru",
            "phone": "8909001212",
            "userName": "user1"
        }
        response = self.client.put(f"/user/{user_id}", json = data, 
            headers = headers, 
            name = "Update user")

        if response.status_code != 200:
           raise FlowException('user not updated')

        # delete user 
        self.client.delete(f"/user/{user_id}",
            headers = headers, 
            name = "Update user")

 
#class WebsiteUser(HttpUser):
#    task_set = UserBehavior
