import { createStore } from "vuex";
import authModule from "./modules/auth/index.js";
import createPersistedState from "vuex-persistedstate";

export default createStore({
  plugins: [createPersistedState()],
  modules: {
    auth: authModule
  },
});
