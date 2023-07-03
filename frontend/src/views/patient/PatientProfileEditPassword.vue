<template>
    <section id="edit-password">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/profil">Mój profil</router-link>
                        <router-link to="/profil/edycja-hasla">Edycja hasła</router-link>
                    </breadcrumbs>
                    <hello-message name="Mariusz" icon-name="clipboard"><template v-slot:info>Tutaj możesz edytować swoje hasło do konta</template></hello-message>
                    <div class="d-flex flex-column align-items-center"> 
                        <div class="col-md-6">
                            <h1>Edycja hasła</h1>
                            <form>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="current-password">Aktualne hasło</label>
                                    <input type="password" class="form-control" id="current-password">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="new-password">Nowe hasło</label>
                                    <input type="password" class="form-control" id="new-password">
                                </div>  
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="repeat-new-password">Powtórz nowe hasło</label>
                                    <input type="password" class="form-control" id="repeat-new-password">
                                </div>  
                                <base-button type="dark">Zmień hasło</base-button>
                                <router-link to="/profil"><base-button type="light">Anuluj</base-button></router-link>
                            </form>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>

export default {
    data(){
        return {
            info: "Oto lista w"
        }
    }

}</script>

<style lang="scss" scoped>
.col-md-6 {
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
    }
    .base-button {
        margin: 30px 10px;

        .span-icon {
            display: inline-block;
            transition: all 0.2s ease-in-out;
        }
    }

    .base-button:hover .span-icon {
        transform: translateX(5px);
    }

</style>