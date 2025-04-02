resource "aws_instance" "codedinner-prod" {
  ami           = "ami-08b5b3a93ed654d19"
  instance_type = "t2.micro"
  key_name = "codedinner-keypair"
  vpc_security_group_ids = ["sg-082cb1f9704c806dd"]

  provisioner "remote-exec" {
    inline = [
      "mkdir -p /home/ec2-user/zips",
    ]
  }
  provisioner "file" {
    source      = "zips/wwwroot.zip"
    destination = "/home/ec2-user/zips/wwwroot.zip"
  }
  provisioner "file" {
    source      = "zips/api.zip"
    destination = "/home/ec2-user/zips/api.zip"
  }

  provisioner "remote-exec" {
    inline = [
      "mkdir -p /home/ec2-user/zips",
      "sudo yum update",
      "sudo yum install -y nginx",
      "mkdir /home/ec2-user/wwwroot",
      "unzip /home/ec2-user/zips/wwwroot.zip -d /home/ec2-user/wwwroot",
    ]
  }

  tags = {
    Name = "codedinner-worker-prod"
  }
  
  provisioner "remote-exec" {
    inline = [
      "sudo yum update",
      "sudo yum install -y dotnet-sdk-8.0",
      "mkdir /home/ec2-user/api",
      "unzip /home/ec2-user/zips/api.zip -d /home/ec2-user/api",
      "sudo systemctl restart nginx"
    ]
  }

  connection {
    type        = "ssh"
    user        = "ec2-user"
    private_key = file("C:\\Users\\denys\\Desktop\\kep\\diplom\\app\\CodeDinner\\.ssh\\codedinner-keypair.pem")
    host        = self.public_ip
  }
}