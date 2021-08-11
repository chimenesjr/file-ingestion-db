provider "google" {
  credentials = "${file("./book-255910-cb84b5f5eb61.json")}"
  project     = "book-255910"
  region      = "us-central1"
  zone        = "us-central1-a"
}

resource "google_compute_instance" "sql-server-db" {

  name         = "sql-server-db"
  machine_type = "n1-standard-1"
  zone         = "us-central1-a"
  tags         = ["allow-sql", "http-server"]

  boot_disk {
    initialize_params {
      image = "ubuntu-os-cloud/ubuntu-1404-trusty-v20160602"
    }
  }

  network_interface {
    network = "default"

    access_config {
      # Ephemeral
    }
  }

  metadata_startup_script = <<SCRIPT
      sudo curl -sSL https://get.docker.com/ | sh
      sudo usermod -aG docker `echo $USER`
      sudo docker run -d -p 80:80 nginx
      sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=hard!@1029' -p 1433:1433 --name sql-server-db microsoft/mssql-server-linux
      SCRIPT

  service_account {
    scopes = ["https://www.googleapis.com/auth/compute.readonly"]
  }
}