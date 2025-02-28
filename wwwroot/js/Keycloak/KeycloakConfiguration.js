



function initilaze() {
    return keycloak.init({ onLoad: 'check-sso' });
}

    //keycloak.init({ onLoad: 'check-sso' }).then( (authenticated)=> {
    //    if (authenticated) {
    //        localStorage.setItem("access_token", keycloak.token);
    //        localStorage.setItem("refresh_token", keycloak.refreshToken);


    //    } else {
    //        keycloak.login({
    //            redirectUri: window.location.origin + '/Home/Index' // Redirect user after login
    //        });
    //    }
    //}).catch( ()=> {
    //    console.error('Failed to initialize Keycloak');
    //    window.location = window.location.origin + '/Home/Error'

    //});




function Logout() {
    keycloak.logout({
        redirectUri: window.location.origin + '/Home/Index' // Redirect user after login
    });
}