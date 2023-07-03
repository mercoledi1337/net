<template>
    <section id="signup">
        <div class="container">
        <div class="row d-flex flex-column align-content-center">
            <div class="col-md-5">
            <h1>Rejestracja</h1>
            <form @submit.prevent="submitForm">
                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="name">Imię</label>
                    <input type="text" class="form-control" id="name" v-model.trim="firstName" required @change="validateFirstName">
                </div>
                <p v-if="errorFirstName"> {{ errorFirstName }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="surname">Nazwisko</label>
                    <input type="text" class="form-control" id="surname" v-model.trim="lastName" required @change="validateLastName">
                </div>
                <p v-if="errorLastName"> {{ errorLastName }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="email">Adres email</label>
                    <input type="email" class="form-control" id="email" v-model.trim="emailAddress" required @change="validateEmailAddress">
                </div>
                <p v-if="errorEmailAddress"> {{ errorEmailAddress }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="pass">Hasło</label>
                    <input 
                        type="password" 
                        class="form-control" 
                        id="pass" 
                        v-model.trim="password" 
                        @change="validatePassword"
                        @input="validatePasswordAfterChange"
                        required
                    >
                </div>
                <p v-if="errorPassword"> {{ errorPassword }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="repeat-pass">Powtórz hasło</label>
                    <input 
                        type="password" 
                        class="form-control" 
                        id="repeat-pass" 
                        v-model.trim="repeatPassword" 
                        @input="validateRepeatedPassword"
                        required 
                    >
                </div>
                <p v-if="errorPasswordsCompare"> {{ errorPasswordsCompare }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="tel">Telefon kontaktowy</label>
                    <input type="tel" class="form-control" id="tel" v-model.trim="phoneNumber" @change="validatePhoneNumber" required>
                </div>
                <p v-if="errorPhoneNumber"> {{ errorPhoneNumber }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="pesel">PESEL</label>
                    <input type="text" class="form-control" id="pesel" v-model.trim="pesel" @change="validatePESEL" required>
                </div>
                <p v-if="errorPESEL"> {{ errorPESEL }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="city">Miejscowość</label>
                    <input type="text" class="form-control" id="city" v-model.trim="city" @change="validateCity" required>
                </div>
                <p v-if="errorCity"> {{ errorCity }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="postal-code">Kod pocztowy</label>
                    <input type="text" class="form-control" id="postal-code" v-model.trim="postalCode" @change="validatePostalCode" placeholder="00-000" required>
                </div>
                <p v-if="errorPostalCode"> {{ errorPostalCode }} </p>

                <div class="form-group d-flex flex-column">
                    <label class="align-self-start" for="street">Ulica i nr budynku</label>
                    <input type="text" class="form-control" id="street" v-model.trim="street" @change="validateStreet" required>
                </div>
                <p v-if="errorStreet"> {{ errorStreet }} </p>

                <div class="form-check d-flex">
                    <input type="checkbox" class="form-check-input" id="regulamin" v-model="regulationsAccepted" @change="validateCheckbox" required>
                    <label class="form-check-label" for="regulamin">Akceptuję <span><router-link class="align-self-start" to="#">regulamin</router-link></span></label>
                </div>
                <p v-if="errorRegulationsAccepted"> {{ errorRegulationsAccepted }} </p>

                <base-button type="dark" :has-icon="true"
                @click="validateInputs"
                >Zarejestruj się</base-button>
                <p>Masz już konto? <router-link to="/logowanie">Zaloguj się</router-link></p>
            </form>
            </div>
        </div>
        </div>
    </section>
</template>

<script>
import axios from 'axios';
import { useToast } from "vue-toastification";

export default {
    name: "SignUpView",

    setup() {
        const toast = useToast();
        return { toast }
    },
    computed: {
        emailAddressRegex: () =>  /^[^@]+@\w+(\.\w+)+\w$/,
        phoneNumberRegex: () => /^\d{9}$/,
        peselRegex: () => /^\d{11}$/,
        postalCodeRegex: () => /^[0-9]{2}-[0-9]{3}/
    },
    data(){
        return {
            firstName: "",
            lastName: "",
            emailAddress: "",
            password: "",
            repeatPassword: "",
            phoneNumber: "",
            pesel: "",
            city: "",
            postalCode: "",
            street: "",
            regulationsAccepted: false,
            
            errorFirstName: null,
            errorLastName: null,
            errorEmailAddress: null,
            errorPassword: null,
            errorPasswordsCompare: null,
            errorPasswordCounter: 0,
            errorPhoneNumber: null,
            errorPESEL: null,
            errorCity: null,
            errorPostalCode: null,
            errorStreet: null,
            errorRegulationsAccepted: null,
            errorEmptyInput: "Pole nie może być puste"

        }
    },
    methods: {
        validateFirstName() {
            this.firstName.length == 0 ? this.errorFirstName = this.errorEmptyInput : 
            this.firstName.length < 2 ? this.errorFirstName = "Podane imię jest zbyt krótkie" : this.errorFirstName = null
        },
        validateLastName () {
            this.lastName.length == 0 ? this.errorLastName = this.errorEmptyInput :
            this.lastName.length < 2 ? this.errorLastName = "Podane nazwisko jest zbyt krótkie" : this.errorLastName = null
        },
        validateEmailAddress () {
            this.emailAddress.length == 0 ? this.errorEmailAddress = this.errorEmptyInput :
            !this.emailAddressRegex.test(this.emailAddress) ? this.errorEmailAddress = "Nieprawidłowy adres email" : this.errorEmailAddress = null;
        },
        validatePassword() {
            this.password.length == 0 ? this.errorPassword = this.errorEmptyInput :
            this.password.length < 8 ? this.errorPassword = "Hasło powinno mieć co najmniej 8 znaków" : this.errorPassword = null
            this.errorPasswordCounter += 1;
        },
        validatePasswordAfterChange(){
            this.errorPasswordCounter >= 1 && this.password.length < 8 ? this.errorPassword = "Hasło powinno mieć co najmniej 8 znaków" : this.errorPassword = null
        },
        validateRepeatedPassword() {
            this.repeatPassword.length == 0 ? this.errorPasswordCompare = this.errorEmptyInput :
            this.password != this.repeatPassword ? this.errorPasswordsCompare = "Hasła muszą być jednakowe" : this.errorPasswordsCompare = null
        },
        validatePhoneNumber() {
            this.phoneNumber.length == 0 ? this.errorPhoneNumber = this.errorEmptyInput :
            !this.phoneNumberRegex.test(this.phoneNumber) ? this.errorPhoneNumber = "Nieprawidłowy numer telefonu" : this.errorPhoneNumber = null
        },
        validatePESEL() {
            this.pesel.length == 0 ? this.errorPESEL = this.errorEmptyInput :
            !this.peselRegex.test(this.pesel) ? this.errorPESEL = "Nieprawidłowy PESEL" : this.errorPESEL = null
        },
        validateCity(){
            this.city.length == 0 ? this.errorCity = this.errorEmptyInput :
            this.city.length < 3 ? this.errorCity = "Nieprawidłowa nazwa miasta" : this.errorCity = null
        },
        validatePostalCode() {
            this.postalCode.length == 0 ? this.errorPostalCode = this.errorEmptyInput :
            !this.postalCodeRegex.test(this.postalCode) ? this.errorPostalCode = "Nieprawidłowy kod pocztowy" : this.errorPostalCode = null
        },
        validateStreet(){
            this.street.length == 0 ? this.errorStreet = this.errorEmptyInput :
            this.street.length < 3 ? this.errorStreet = "Nieprawidłowa nazwa ulicy" : this.errorStreet = null
        },
        validateCheckbox(){
            !this.regulationsAccepted ? this.errorRegulationsAccepted = "Regulamin musi być zaakceptowany" : this.errorRegulationsAccepted = false;
        },
        
        async submitForm(){
            const response = await axios.post('https://localhost:7121/api/Uzytkownik/rejestracja', {
                firstName: this.firstName,
                lastName: this.lastName,
                emailAddress: this.emailAddress,
                password: this.password,
                phoneNumber: this.phoneNumber,
                pesel: this.pesel,
                city: this.city,
                postalCode: this.postalCode,
                streetAddress: this.street
            }); 
            if(response.status == 200) {
                this.toast.success("Zarejestrowano pomyślnie", {
                    timeout: 2500,
                    position: "bottom-right",
                });
                this.$router.replace({name: "login"});
            }
        },
        validateInputs(){
            this.validateFirstName ();
            this.validateLastName ();
            this.validateEmailAddress ();
            this.validatePassword() ;
            this.validatePasswordAfterChange()
            this.validateRepeatedPassword();    
            this.validatePhoneNumber();
            this.validatePESEL();
            this.validateCity();
            this.validatePostalCode();
            this.validateStreet();
            this.validateCheckbox();
            /* trzeba sprawdzac czy ta walidacja przechodzi, bo mozna zrobic konto z nieprawidlowym powtorz haslo
            if(this.errorFirstName != null || this.error.lastName != null || this.error.emailAddress != null) {
                this.toast.error("Popraw błędy w formularzu", {
                    timeout: 2500,
                    position: "bottom-right",
                });
            }
            */
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

    .col-md-5 {
        form {
            .form-group {
                margin: 13px 0;

                label {
                    font-weight: 500;
                    font-size: 14px;
                    line-height: 30px;
                    letter-spacing: -0.001em;
                    color: $secondary;

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

            .form-check {
                margin: 30px 0;
                input {
                    border-color: $secondary;
                    &:checked {
                        background-color: $teal;
                        border-color: $secondary;
                    }
                }

                label {
                    margin-left: 10px;
                    font-weight: 500;
                    color: $secondary;

                    span {

                        a {
                            color: $teal;
                        }
                        
                    }
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
  