<?php

require_once __DIR__ . '/../model/eventservice.class.php';
require_once __DIR__ . '/../app/database/db.class.php';

class LoginController{
	
	public function index(){
		$message = '';
		require_once __DIR__ . '/../view/main.php';
	}

	public function try_login(){
        if( isset( $_POST['txtUsername'] ) && isset( $_POST['txtPassword'] ) ){
			$password =$_POST['txtPassword'];
			$passed = false;
			$ls = new EventService;
			$user = $ls->getUserByUsername( $_POST['txtUsername'] );
			if(password_verify($password, $user->password)){
				if($user->registered){
					$_SESSION['username'] =  $_POST['txtUsername'];
					$message = 'Dobrodosli '.$user->username;
					$is_admin = $ls->checkAdminshipByUsername($_POST['txtUsername']);
					if( $is_admin == 1 ){
						$_SESSION['admin'] = true;
					}
					require_once __DIR__ . '/../view/main.php';
					$passed = true;
				}
				else{
					$message = 'Verifikacija preko email-a nije izvrsena!';
					require_once __DIR__ . '/../view/prijava.php';
				}
			}
			if(!$passed){
				$message = 'Pogresan username ili password !';
				require_once __DIR__ . '/../view/prijava.php';
			}
        }
	else{
		$message = 'ERROR 404';
		require_once __DIR__ . '/../view/prijava.php';
	}
    	}
	
	public function login(){
		$message = '';
		require_once __DIR__ . '/../view/prijava.php';
	}

	public function register(){
		$message = '';
		require_once __DIR__ . '/../view/registracija.php';
	}

	public function try_register(){
        if( isset( $_POST['txtIme'] ) && isset( $_POST['txtPrezime'] ) && isset( $_POST['txtUsername'] ) && isset( $_POST['txtPassword'] ) && isset( $_POST['txtEmail'] ) ){
			$pattern = "/^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$/i";
			if( !preg_match($pattern,$_POST['txtIme']) ){
				$message = 'Pogresno ime!';
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			if( !preg_match($pattern,$_POST['txtPrezime']) ){
				$message = 'Pogresno prezime!';
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			$pattern = "/^[a-z0-9_-]{3,15}$/i";
			if( !preg_match($pattern,$_POST['txtUsername']) ){
				$message = 'Pogresan username! Unesite 3-15 znakova!';
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			$pattern = "/^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,})$/i";
			if( !preg_match($pattern,$_POST['txtEmail']) ){
				$message = 'Pogresna email adresa!';
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			$pattern = "/(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/i";
			if( !preg_match($pattern,$_POST['txtPassword']) ){
				$message = 'Pogresan password! Unesite najmanje 8 znakova( bar jedno slovo i bar jedan broj )!';
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			$ls = new EventService;
			$userList = $ls->getAllUsers();
			foreach($userList as $user){
				if( $user->username == $_POST['txtUsername'] ){
					$message = 'Username vec postoji!';
					require_once __DIR__ . '/../view/registracija.php';
					exit(0);
				}
				if( $user->email == $_POST['txtEmail'] ){
					$message = 'Email adresa vec iskoristena!';
					require_once __DIR__ . '/../view/registracija.php';
					exit(0);
				}
			}
			$suc = $ls->register($_POST['txtIme'], $_POST['txtPrezime'], $_POST['txtUsername'], $_POST['txtPassword'], $_POST['txtEmail']);
			if(!$suc){
				$message = "Greska u slanju mail-a!";
				require_once __DIR__ . '/../view/registracija.php';
				exit(0);
			}
			$message = 'Za dovrsetak registracije pritisnite na link koji smo Vam poslali na vasu email adresu (moguce u spam folderu).';
			require_once __DIR__ . '/../view/prijava.php';
        }
        else{
            $message = 'Greska!';
            require_once __DIR__ . '/../view/registracija.php';
        }
    }

}
?>