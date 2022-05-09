<template>
  <div class="login-container">
    <div class="form-group">
      <label for="username">Username:</label>
      <input id="username" class="form-control" v-model.trim="username"/>
    </div>
    <div class="form-group">
      <label for="password">Password:</label>
      <input id="password" class="form-control" v-model.trim="password"/>
    </div>

    <button class="btn btn-primary" @click="login">Submit</button>
  </div>

  <div v-if="!!user" class="user-data">
    <ul>
      <li v-for="key of Object.keys(user)">
        <span>{{ key }}:</span> {{ user[key] }}
      </li>
    </ul>
  </div>

  <div class="result-message"
       v-bind:class="{
        'error': loginResult.ResultCode !== 0,
        'success': loginResult.ResultCode === 0,
        'active': isLoginResultDisplayed }">
    {{ loginResult.ResultMessage }}
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: "LoginForm",
  data() {
    return {
      username: null,
      password: null,
      loginResult: {'ResultCode': null, 'ResultMessage': null},
      isLoginResultDisplayed: false,
      user: null
    }
  },
  methods: {
    login() {
      this.user = null;
      this.loginResult = {'ResultCode': null, 'ResultMessage': null};
      axios.post('/Home/Login', {username: this.username, password: this.password})
          .then(response => {
            if (!response) {
              this.loginResult.ResultMessage = 'Bad response';
              return;
            }

            if (response.status !== 200) {
              this.loginResult.ResultMessage = 'Bad request';
              return;
            }

            if (response.data.ResultCode !== undefined) {
              this.loginResult = response.data;
              return;
            }

            if (typeof response.data === 'string') {
              this.loginResult.ResultCode = null;
              this.loginResult.ResultMessage = response.data;
              return;
            }

            this.user = response.data;
            this.loginResult.ResultCode = 0;
            this.loginResult.ResultMessage = 'Success';
          })
          .catch(error => {
            console.log(error);
            this.loginResult.ResultCode = null;
            this.loginResult.ResultMessage = error.message;
          })
          .finally(() => {
            this.showLoginResult();
          });
    },
    showLoginResult() {
      this.isLoginResultDisplayed = true;
      setTimeout(() => {
        this.isLoginResultDisplayed = false;
      }, 5000);
    }
  }
}
</script>

<style>
.login-container {
  width: 30rem;
  margin: auto;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  padding: 2rem;
}

.login-container .form-group {
  text-align: left;
}

.login-container .form-group label {
  font-weight: 600;
}

.user-data {
  margin: auto;
  text-align: left;
}

.user-data span {
  font-weight: 600;
}

.result-message {
  color: transparent;
  background: transparent;
  position: absolute;
  bottom: 0;
  right: 50px;
  opacity: 0;
  transition: .3s ease-in-out;
  padding: 1rem;
}

.result-message.active {
  color: #FFFFFF;
  opacity: 1;
  bottom: 50px;
}

.result-message.active.success {
  background: green;
}

.result-message.active.error {
  background: red;
}
</style>