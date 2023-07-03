<template>
    <section id="login">
        <div class="container">
            <div class="row d-flex justify-content-center">
            <div class="inner-container col-sm-10 col-md-8 col-lg-6">
                <h1>Logowanie</h1>
                <form @submit.prevent="submitForm()">
                    <div class="form-group d-flex flex-column align-items-center">
                        <label for="email">Adres email</label>
                        <input type="email" class="form-control" id="email" v-model="emailAddress">
                    </div>

                    <div class="form-group d-flex flex-column align-items-center">
                        <label for="password">Hasło</label>
                        <input type="password" class="form-control" id="password" v-model="password">
                    </div>
                    <p v-if="error"> {{ error }} </p>
                    <base-button type="dark" :has-icon="true">Zaloguj się</base-button>
                    <p>Nie masz konta? <router-link to="/rejestracja">Zarejestruj się</router-link></p>
                </form>
            </div>
            </div>
        </div>
    </section>
</template>
  
<script>
import axios from 'axios';
import jwt_decode from "jwt-decode";
import { mapGetters } from 'vuex'
import { useToast } from "vue-toastification";
// import store from "@/store"''

export default {
    name: "LoginView",

    setup() {
        const toast = useToast();
        return { toast }
    },

    data(){
        return {
            emailAddress: "",
            password: "",
            error: null,
            isLoggedIn: false,
            accountType: null,
            user_object: null,
        }
    },
    computed: {
        ...mapGetters(['isLoggedIn', 'accountType'])
    },
    methods: {    
        async submitForm(){
            await axios.post('https://localhost:7121/api/Uzytkownik/login', {
                emailAddress: this.emailAddress,
                password: this.password,
            })
            .then((response) => {
                localStorage.token = response.data;
                const responseDecoded = jwt_decode(response.data);
                localStorage.decodedToken = responseDecoded;
                this.accountType = responseDecoded.role;
                this.$store.dispatch('user', { id: responseDecoded.emailAddress, firstName: responseDecoded.firstName, lastName: responseDecoded.lastName, accountType: responseDecoded.role});
            })
            .then(() => {
                this.toast.success("Zalogowano pomyślnie", {
                    timeout: 2500,
                    position: "bottom-right",
                });
                this.isLoggedIn = true;
                this.redirectAfterSuccessfullLogin();
            })
            .catch((error) => {
                this.toast.error("Nieprawidłowe dane logowania", {
                    timeout: 2500,
                    position: "bottom-right",
                });
                this.error = "Nieprawidłowe dane logowania";
                console.log(error);
            });

        },
        redirectAfterSuccessfullLogin(){
            if(this.isLoggedIn && this.accountType == 'Patient'){
                this.$router.replace({name: 'home'});
            }
            else if(this.isLoggedIn && this.accountType == 'Doctor'){
                this.$router.replace({name: 'patients-list'})
            }
            else if(this.isLoggedIn && this.accountType == 'Receptionist'){
                this.$router.replace({name: 'receptionist-patients-visits'})
            }
        },
        async fetchUserData(id) {
            const response = await axios.get(`https://localhost:7121/api/Uzytkownik/Dane-personalne/${id}`);
            return response.data.data;
        }
    }

};
</script>

<style lang="scss" scoped>
    .row {
        p {
            color: $button-dark;
            font-style: normal;
            font-weight: 600;
            font-size: 15px;
            line-height: 22px;
            letter-spacing: -0.001em;
            color: $secondary;
            
            a {
                color: $teal;
            }
        }
    } 

    div.inner-container  {
        form {
            div.form-group {
                margin: 13px 0;

                label {
                    font-weight: 500;
                    font-size: 14px;
                    line-height: 30px;
                    letter-spacing: -0.001em;
                    color: $secondary;
                    align-self: flex-start;
                }

                input {
                    box-sizing: border-box;
                    border: 1px solid #5F6D7E;
                    border-radius: 8px;
                    display: flex;
                    padding: 8px 18px;
                    gap: 16px;
                    background: #F8F9FB;
                }
            }
        
        }
        h1 {
            margin: 60px 0;
            font-weight: 700;
            font-weight: 700;
            font-size: 52px;
            line-height: 60px;
            text-align: center;
            letter-spacing: -0.01em;
            color: $button-dark;
        }
        div + p {
            margin: 0;
            color: $button-red;
        }

    }
</style>
  