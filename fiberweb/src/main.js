// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import fiber from './components/fiber/fiber'
import login from './components/login/login'
import login1 from './components/login/login1'
import profile from './components/profile/profile'
import user from './components/user/user'
import JsonExcel from "vue-json-excel";
import VueExcelXlsx from "vue-excel-xlsx";
//bootstrap
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
//ElementUI
import ElementUI from 'element-ui';
import locale from 'element-ui/lib/locale/lang/en';
import 'element-ui/lib/theme-chalk/index.css';
//Fontawesome
import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/js/all.js'
//Dropdown component
import { DropDownListPlugin } from '@syncfusion/ej2-vue-dropdowns';

Vue.use(DropDownListPlugin);
Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(ElementUI, { locale })
Vue.component("downloadExcel", JsonExcel);
Vue.use(VueExcelXlsx);
 
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App, fiber, login, profile, user, login1 },
  template: '<app/>'
})
