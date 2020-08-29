<?php
    class User{
        protected  $id, $name, $surname, $username, $email, $password, $registration_sequence, $registered, $admin;

        public function __construct($id, $name, $surname, $username, $email, $password, $registration_sequence, $registered, $admin){
            $this->id = $id;
            $this->username = $username;
            $this->name = $name;
            $this->surname = $surname;
            $this->email = $email;
            $this->password= $password;
	    $this->registration_sequence = $registration_sequence;
	    $this->registered = $registered;
	    $this->admin = $admin;
        }

        public function __get( $property ){
            if( property_exists( $this, $property ) )
                return $this->$property; 
        }

        public function __set( $property, $value ){
            if( property_exists( $this, $property ) )
                $this->$property = $value;
            
            return $this;
        }
    }

?>